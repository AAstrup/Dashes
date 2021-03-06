﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserHoming : ITrigger
{

    public LaserHoming(float _dmg, float rotation, Vector2 startPos, IUnit player)
    {
        Rot = rotation;
        Pos = startPos;
        lifeTimeSpan = 40f; //Bliver deleted af Bossen
        dmg = _dmg;
        triggerRange = 0.2f;
        radius = triggerRange;
        gmjPrefabName = "LaserHoming";
        speed = new Vector2(Mathf.Cos(Rot * Mathf.Deg2Rad), Mathf.Sin(Rot * Mathf.Deg2Rad)) * 5f;
        targets = new List<IUnit>() { player };
        effectTrigger = ParticleEffectHandler.particleType.effect_hit;
        effectTimespan = ParticleEffectHandler.particleType.effect_hit;

        Init();


    }

    protected override void Trigger(IUnit victim)
    {
        victim.Damage(dmg);
        base.Trigger(victim);
    }

    protected override void TriggerWall()
    {
        var info = References.instance.colSystem.CollidesWithWall(this);

        speed = Vector2.Reflect(speed, info.GetCollisionNormal());

        Pos = info.GetFinalPos();

        UpdateRot();

        Debug.Log(speed.magnitude);

        //Pos += temp*Time.deltaTime*2;

    }


}
