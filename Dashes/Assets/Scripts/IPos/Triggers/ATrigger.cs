﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class ITrigger : Position {

    protected List<IUnit> targets;
    protected Vector2 speed;
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

    float debugID;

    public virtual void Init()
    {
        debugID = UnityEngine.Random.Range(0,1000000f);
        spawnTime = Time.time;
        GBref = References.instance.CreatePrefabWithParameters(gmjPrefabName, new Vector3(Pos.x, Pos.y, 0), new Vector3(0, 0, Rot));
        References.instance.triggerHandler.AddTrigger(this);
    }

    // Update is called once per frame
    public void Update () {
        //Debug.Log("UPDATED AT " + Time.time + ", id: " + debugID + " at pos " + Pos.ToString());
        var target = targets.Find(typ => (triggerRange + typ.radius) > Vector2.Distance(Pos, typ.Pos));
        if (target != null)
            Trigger(target);
        else if (Time.time > (spawnTime + lifeTimeSpan))
            TimeLeft();
        if(speed.magnitude != 0)
        {
            Move();
            GBref.transform.position = Pos;
            //GBref.transform.localScale = new Vector3(Scale, Scale,1); //Kaldes i metoder for sig for at spare :0)
            //GBref.transform.rotation = Quaternion.Euler(0, 0, Rot);
        }
    }

    public void UpdateRot()
    {
        Rot = GetAngle( speed);
        GBref.transform.rotation = Quaternion.Euler(0, 0, Rot);
    }
    public void UpdateRot(float rot)
    {
        GBref.transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    public void UpdateScale()
    {
        GBref.transform.localScale = new Vector3(Scale, Scale, 1);
    }

    float GetAngle(Vector3 targetpos)
    {
        return Mathf.Atan2(targetpos.y - Pos.y, targetpos.x - Pos.x) * 180 / Mathf.PI;
    }

    protected virtual void Move()
    {
        Pos += speed * Time.deltaTime;
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

    public virtual void Delete()
    {
        References.instance.triggerHandler.RemoveTrigger(this);
        References.instance.DestroyGameObject(GBref);
    }
}
