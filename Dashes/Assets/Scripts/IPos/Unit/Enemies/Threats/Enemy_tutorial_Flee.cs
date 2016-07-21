using UnityEngine;
using System.Collections;

public class Enemy_tutorial_Flee : EnemyRanged
{
    public Enemy_tutorial_Flee(IUnit player)
    {
		PrepareTime = 0.25f;
		Cooldown = 0.45f;
		TargetUnit = player;
        HealthMax = 20;
        HealthCurrent = 20;
        MovementSpeedBase = 0.7f;
		ActHitRange = 6;
		TargetDistanceMax = 6;
		TargetDistanceMin = 3;
        reviveTypeString = "Enemy_tutorial_Flee";
        EnemyConstructor();

    }
}
