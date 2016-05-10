using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public abstract class IUnit : Position {

    public float HealthMax;
    public float HealthCurrent;
    public float MovementSpeedBase;
    public float MovementSpeedCurrent;

    public float tenacity = 0f;//goes from 0 - 1
    public bool Stunned;
    public float Slow;
    public float Boost;
    public List<Effect> Effects; 

    public Vector2 Scale;
    public float Rot;

    public bool Damaged = false;

    public GameObject GBref;

    public bool Invulnerable = false;

    public virtual void Update()
    {
        Effects.ForEach(typ => typ.Update(this));
        Pos = References.instance.colSystem.CollidesWithWall(this).GetFinalPos();
        GBref.transform.position = Pos;
        GBref.transform.localScale = Scale;
        GBref.transform.rotation = Quaternion.Euler(0, 0, Rot);
    }

    public virtual void Damage(float amount)
    {
        if (!Invulnerable)
        {
            HealthCurrent -= amount;

            if (HealthCurrent <= 0)
            {
                Die();
            }
        }
    }

    public virtual void Heal(float amount)
    {
        HealthCurrent = Mathf.Min(HealthCurrent+amount,HealthMax);
    }

    public virtual void Die()
    {
        References.instance.UnitHandler.Units.Remove(this);
        References.instance.DestroyGameObject(GBref);
    }

    public void GenericConstructor(GameObject prefab)
    {
        MovementSpeedCurrent = MovementSpeedBase;
        Effects = new List<Effect>();
        Stunned = false;
        Slow = 0f;
        GBref = References.instance.CreateGameObject(prefab);
        Scale = Vector2.one;
    }

    public void UpdateMovementSpeed()
    {
        MovementSpeedCurrent = MovementSpeedBase*(1 - Slow)*(1 + Boost);
    }

}
