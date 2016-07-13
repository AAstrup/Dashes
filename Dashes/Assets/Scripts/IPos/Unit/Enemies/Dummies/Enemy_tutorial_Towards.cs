﻿using UnityEngine;
using System.Collections;

public class Enemy_tutorial_Towards : EnemyMelee {

    public Enemy_tutorial_Towards(IUnit player)
    {
        target = player;
        HealthMax = 10;
        HealthCurrent = 10;
        MovementSpeedBase = 1f;
        hitParticleMin = 20;
        attackChargeTime = 0.5f;
        damage = 0f;

        reviveTypeString = "Enemy_tutorial_Towards";
        FinishConstructor();
    }

    public override void Fire(Vector2 pos)
    {
    }
}
