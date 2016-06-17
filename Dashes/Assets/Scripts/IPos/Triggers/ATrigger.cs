using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class ITrigger : Position {

    protected List<IUnit> targets;
    protected float movementSpeed;
    protected float Rot;
    protected float Scale = 1f;
    protected float dmg;
    protected float lifeTimeSpan;
    private float spawnTime;
    protected float triggerRange;
    protected string gmjPrefabName;
    protected ParticleEffectHandler.particleType effectTrigger = ParticleEffectHandler.particleType.effect_none;
    protected int effectTriggerEmitAmount = 10;
    protected ParticleEffectHandler.particleType effectTimespan = ParticleEffectHandler.particleType.effect_none;
    protected int effectTimespanEmitAmount = 10;

    public GameObject GBref;

    public virtual void Init()
    {
        spawnTime = Time.time;
        References.instance.triggerHandler.triggers.Add(this);
        GBref = References.instance.CreatePrefabWithParameters(gmjPrefabName, new Vector3(Pos.x, Pos.y, 0), new Vector3(0, 0, Rot));
    }

    // Update is called once per frame
    public void Update () {
        var target = targets.Find(typ => (triggerRange + typ.radius) > Vector2.Distance(Pos, typ.Pos));
        if (target != null)
            Trigger(target);
        else if (Time.time > (spawnTime + lifeTimeSpan))
            TimeLeft();
        if(movementSpeed != 0)
        {
            Move();
            GBref.transform.position = Pos;
            GBref.transform.localScale = new Vector3(Scale, Scale,1);
            GBref.transform.rotation = Quaternion.Euler(0, 0, Rot);
        }
    }

    protected virtual void Move()
    {
        var currentRotRAD = Rot * Mathf.Deg2Rad;
        var vector = new Vector2(Mathf.Cos(currentRotRAD), Mathf.Sin(currentRotRAD)) * movementSpeed;
        Pos += vector * Time.deltaTime;
        if (References.instance.colSystem.CollidesWithWall(this).Collided())
            TriggerWall();
    }

    protected virtual void Trigger(IUnit victim)
    {
        References.instance.DestroyGameObject(GBref);
        if (effectTrigger != ParticleEffectHandler.particleType.effect_none)
            References.instance.particleHandler.Emit(effectTrigger, effectTriggerEmitAmount, Pos);
        Delete();
    }
    protected virtual void TimeLeft()
    {
        References.instance.DestroyGameObject(GBref);
        if (effectTimespan != ParticleEffectHandler.particleType.effect_none)
            References.instance.particleHandler.Emit(effectTimespan, effectTriggerEmitAmount, Pos);
        Delete();
    }

    protected virtual void TriggerWall()
    {
        References.instance.DestroyGameObject(GBref);
        if (effectTrigger != ParticleEffectHandler.particleType.effect_none)
            References.instance.particleHandler.Emit(effectTrigger, effectTriggerEmitAmount, Pos);
        Delete();
    }

    protected virtual void Delete()
    {
        References.instance.DestroyGameObject(GBref);
        References.instance.triggerHandler.triggers.Remove(this);
    }
}
