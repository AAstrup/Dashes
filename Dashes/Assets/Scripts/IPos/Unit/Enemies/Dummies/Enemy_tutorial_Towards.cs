using UnityEngine;
using System.Collections;

public class Enemy_tutorial_Towards : EnemyMelee {

    public Enemy_tutorial_Towards(IUnit player)
    {
		TargetUnit = player;
        HealthMax = 10;
        HealthCurrent = 10;
        MovementSpeedBase = 1f;
        hitParticleMin = 20;
		PrepareTime = 0.5f;
		ActValue = 0f;

        reviveTypeString = "Enemy_tutorial_Towards";
		EnemyConstructor();
    }

    public override void Act(Vector2 pos)
    {
    }
}
