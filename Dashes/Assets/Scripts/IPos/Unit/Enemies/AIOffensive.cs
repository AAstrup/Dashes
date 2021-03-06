﻿/*using UnityEngine;
using System.Collections;
using System;

public class AIOffensive : IEnemy {
    //Behavior
    public float attackChargeTime = 0.25f;//Time from starting to attack to attacking
    private float attackChargeTimeLeft = -1f;//Must be less than 0 if it     not, fire on spawn

    private float attackAppliedMinTime = 0.2f;//The min time that must pass to inOrder to damage the player again.
    private float attackAppliedTimeLeft = -1f;//Time left in order to apply damage to player again when hitting

    public float moveAttackSpeedPercentage = 0f; //Percent of Movespeed having while attacking, 0 -> 1 (100%)
    protected float cdTimeLeft;
    float fireEndTime;

    protected bool fleeing = false;
    protected Vector2 deltaPos;
    protected float currentRot;

    //Attributes
    public float radius = 0.5f;
    public float continueFireTimeSeconds = 0f;//If higher than 0, it will continue attacking
    public float hitRange = 1f; //max range for attacking
    public float engageRange = 1f;//Max range for starting attack
    public float fleeRange = 0f; //flee if within range
    IUnit lastTarget;

    protected void FinishConstructor()
    {
        GenericConstructor(References.instance.PrefabLibrary.Prefabs[reviveTypeString]);
    }

    //Condition for starting preparation, by default measured by distance
    protected virtual bool EngageCondition(IUnit target)
    {
        var dist = Vector2.Distance(Pos, target.Pos);
        if (dist < fleeRange)
        {
            fleeing = true;
            return false;
        }
        fleeing = false;
        if (engageRange < dist)
            return false;
        return true;
    }


    // Update is called once per frame
    public virtual bool CanFire (IUnit target) { 
        if (EngageCondition(target))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void Search(IUnit target)//  
    {
        SetAngle(target.Pos);
        Move(MovementSpeedCurrent);
        SetVisualColors(new Color(1f,1f,0),"Search");
    }

    //Overlapping
    private void OverlappingFix()
    {
        for (int i = 0; i < References.instance.RoomHandler.aliveEnemies.Count; i++)
        {
            if (References.instance.RoomHandler.aliveEnemies[i] == this)
                continue;
            if ((radius + References.instance.RoomHandler.aliveEnemies[i].radius) > Vector2.Distance(this.Pos, References.instance.RoomHandler.aliveEnemies[i].Pos))
            {
                var vector = (Pos - References.instance.RoomHandler.aliveEnemies[i].Pos);
                Pos = Pos + vector/48;
                References.instance.RoomHandler.aliveEnemies[i].Pos = References.instance.RoomHandler.aliveEnemies[i].Pos - vector/48;
                return;
            }
        }
    }

    protected virtual void StartPreparation(Vector2 targetpos)
    {
        SetAngle(targetpos);
        state = AIState.Preparing;
        deltaPos = targetpos - Pos;
        deltaPos = deltaPos.normalized * hitRange;
        attackChargeTimeLeft = attackChargeTime;
        SetVisualColors(new Color(1f, 0.5f, 0f),"Starting preparation");
    }

    protected void StartFire()
    {
        Fire(deltaPos + Pos);
        SetVisualColors(new Color(1f, 0, 0), "Starting fire");
        if (continueFireTimeSeconds > 0)
        {
            fireEndTime = Time.time + continueFireTimeSeconds;
            state = AIState.Attacking;
        }
        else
        {
            StartCoolingDown();
        }
    }

    public virtual void StartCoolingDown()
    {
        state = AIState.CoolingDown;
        cdTimeLeft = cd;
        SetVisualColors(new Color(1f, 1f, 1f),"Starting CD");
    }

    // Update is called once per frame
    protected void Update(IUnit target)
    {
        base.Update();
        OverlappingFix();
        if (target == null)
            return;
        lastTarget = target;
        attackAppliedTimeLeft -= Time.deltaTime;

        if (state == AIState.Stunned)
        {
            //Handled in IUnit
        }
        else if(state == AIState.Preparing) //Still preparating
        {
            attackChargeTimeLeft -= Time.deltaTime;
            if (attackChargeTimeLeft < 0)//Finished preparation, start attacking
                StartFire();
        }
        else if(state == AIState.Attacking) //Continue attacking
        {
            Move(moveAttackSpeedPercentage);
            Fire(Pos + deltaPos);
            if (Time.time >= fireEndTime)//Done attacking
                StartCoolingDown();
        }
        else if(state == AIState.CoolingDown)//if (CanFire())//Start attacking
        {
            cdTimeLeft -= Time.deltaTime;
            if (cdTimeLeft < 0)
                state = AIState.Searching;
        }
        else if(state == AIState.Searching)
        {
            if (CanFire(target))
                StartPreparation(target.Pos);
            else
                Search(target);
        }
    }


    public override void SetStunned(bool v)
    {
        if (v)
        {
            state = AIState.Stunned;
            SetVisualColors(new Color(1f, 1f, 1f), "Stunned");
        }
        else
        {
            state = AIState.Searching;
        }
        base.SetStunned(v);
    }

    public virtual void Fire(Vector2 pos) { }


    /// <summary>
    /// Used to ensure that the player is not damaged every frame but within a margin.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="target"></param>
    public void DamagePlayer(float damage, IUnit target)
    {
        if (attackAppliedTimeLeft <= 0)
        {
            attackAppliedTimeLeft = attackAppliedMinTime;
            target.Damage(damage);
        }
    }

    public override void Revive()
    {
        base.Revive();
    }
}
*/