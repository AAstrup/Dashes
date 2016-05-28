﻿using UnityEngine;
using System.Collections;

public class Charger : EnemyMeleeStupid {

    public Charger(IUnit player)
    {
        target = player;
        HealthMax = 20;
        HealthCurrent = 20;
        MovementSpeedBase = 2f;
        moveAttackSpeedPercentage = MovementSpeedBase;
        fireTime = 2f;
        cd = 3f;
        hitParticleASecond = 100;
        startAttackRange = 3f;
        attackChannelingTime = 0f;
        hitEffect = ParticleEffectHandler.particleType.effect_hit;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Charger"]);
    }
}