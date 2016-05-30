using UnityEngine;
using System.Collections;
using System;

public class AINavigation : IUnit {
    public float attackChannelingTime = 0.25f;
    private float attackChannelingTimeLeft = -1f;//Must be less than 0 if it     not fire on spawn
    public float radius = 0.5f;
    public float fireTime = 0f;//If higher than 0, it will continue attacking
    public float cd = 1f;//CD betweem
    public float rotationSpeed = 1.75f;//Speed of rotation
    public float moveAttackSpeedPercentage = 0f; //Percent of Movespeed having while attacking, 0 -> 1 (100%)
    public float hitRange = 1f; //max range for attacking
    public float startAttackRange = 1f;//Max range for starting attack
    public float fleeRange = 0f; //flee if within range

    protected bool fleeing = false;
    protected float lastTimeTimeCD;
    float fireEndTime;
    protected Vector2 deltaPos;
    float maxRotationAmount = 100f;
    protected float currentRot;

    void SetAngle (Vector3 otherpos) {
        //float currentRot = transform.eulerAngles.z;
        var targetRot = Mathf.Atan2(otherpos.y - Pos.y, otherpos.x - Pos.x) * 180 / Mathf.PI;
        //float rotAmount = maxRotationAmount * rotationSpeed * Time.deltaTime;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(0,0,targetRot), rotAmount);
        currentRot = targetRot;
        Rot = targetRot;
    }

    void Move(float ms)
    {
        //Vector2 dir = new Vector2(Mathf.Cos(currentRot), Mathf.Sin(currentRot)) * Mathf.Rad2Deg;
        //transform.Translate(dir.normalized * Time.deltaTime * ms);
        var currentRotRAD = currentRot * Mathf.Deg2Rad;
        var vector = new Vector2(Mathf.Cos(currentRotRAD), Mathf.Sin(currentRotRAD)) * ms;
        if (fleeing)
            vector *= -1;
        Pos += vector * Time.deltaTime;
    }

    bool Within(IUnit target)
    {
        var dist = Vector2.Distance(Pos, target.Pos);
        if (dist < fleeRange)
        {
            fleeing = true;
            return false;
        }
        fleeing = false;
        if (startAttackRange < dist)
            return false;
        return true;
    }


    // Update is called once per frame
    public bool CanFire (IUnit target) {
        OverlappingFix();
           
        if (Within(target))
        {
            Move(moveAttackSpeedPercentage * MovementSpeedCurrent);
            return true;
        }
        else
        {
            SetAngle(target.Pos);
            Move(MovementSpeedCurrent);
            return false;
        }
    }

    private void OverlappingFix()
    {
        for (int i = 0; i < References.instance.RoomHandler.aliveEnemies.Count; i++)
        {
            if (References.instance.RoomHandler.aliveEnemies[i] == this)
                continue;
            if ((radius + References.instance.RoomHandler.aliveEnemies[i].radius) > Vector2.Distance(this.Pos, References.instance.RoomHandler.aliveEnemies[i].Pos))
            {
                var vector = (Pos - References.instance.RoomHandler.aliveEnemies[i].Pos);
                Pos = Pos + vector/2;
                References.instance.RoomHandler.aliveEnemies[i].Pos = References.instance.RoomHandler.aliveEnemies[i].Pos + vector/2;
                return;
            }
        }
    }

    protected bool CanFire()
    {
        if (Time.time > lastTimeTimeCD)
            return true;
        else
            return false;
    }

    void PrepareFire(Vector2 targetpos)
    {
        SetAngle(targetpos);
        lastTimeTimeCD = Time.time + cd;
        deltaPos = targetpos - Pos;
        deltaPos = deltaPos.normalized * hitRange;
        attackChannelingTimeLeft = attackChannelingTime;
    }

    void StartFire()
    {
        Fire(deltaPos + Pos);
        if (fireTime > 0)
            fireEndTime = Time.time + fireTime + attackChannelingTime;
    }

    // Update is called once per frame
    protected void Update(IUnit target)
    {
        base.Update();
        if (Stunned)
            return;

        if(attackChannelingTimeLeft >= 0)
        {
            attackChannelingTimeLeft -= Time.deltaTime;
            if (attackChannelingTimeLeft < 0)
                StartFire();
        }
        else if (Time.time < fireEndTime)
        {
            Move(moveAttackSpeedPercentage);
            Fire(Pos + deltaPos);
        }
        else if (CanFire())
        {
            if (CanFire(target))
                PrepareFire(target.Pos);
        }
    }

    public virtual void Fire(Vector2 pos) { }

    void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        References.instance.RoomHandler.UnitDied(this);
        base.Die();
    }
}
