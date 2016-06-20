﻿using UnityEngine;
using System.Collections;

public class Archer : EnemyRanged {

    public Archer (IUnit player)
    {
        attackChargeTime = 0.25f;
        cd = 0.45f;
        target = player;
        HealthMax = 20;
        HealthCurrent = 20;
        MovementSpeedBase = 0.7f;
        hitRange = 6;
        engageRange = hitRange;
        fleeRange = 3;
        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Archer"]);
    }
}
