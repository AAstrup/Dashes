﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ParticleEffectHandler
{
    public Dictionary<particleType, ParticleEffect> particles;

    public void Init()
    {
        particles = new Dictionary<particleType, ParticleEffect>();
        foreach (KeyValuePair<string, GameObject> entry in References.instance.PrefabLibrary.Prefabs)
        {
            if(entry.Key.Contains("effect_"))
                particles.Add((particleType)Enum.Parse(typeof(particleType), entry.Key), new ParticleEffect
                (References.instance.CreatePrefabWithParameters(entry.Key, new Vector3(0, 0, 0), new Vector3(0, 0, 0)).GetComponent<ParticleSystem>()));
        }
        Emit(particleType.effect_hit, 1, new Vector2(0, -10), 0);
        Emit(particleType.effect_hit, 1, new Vector2(2, -10), 90);
        Emit(particleType.effect_hit, 1, new Vector2(4, -10), 180);
        Emit(particleType.effect_hit, 1, new Vector2(6, -10), 270);
    }

    public void Emit(particleType type,int amount,Vector2 pos,float rot = 0)
    {
        if (type == particleType.effect_none)
            throw new Exception("Particle type is set to none but still spawned?");
        particles[type]._pSystem.startRotation = Mathf.Deg2Rad * (360 - rot);//Vi rotater mod uret, mens de gør med uret
        particles[type].Emit(amount,pos);
    }

    public enum particleType { effect_none, effect_hit, effect_slashEffect, effect_explosion, effect_orangeglow, effect_redglow, effect_whiteglow, effect_flower, effect_halo, effect_dash, effect_bleed, effect_boss1_laser, effect_boss1_lasterBounce }
}
