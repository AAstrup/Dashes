using UnityEngine;
using System.Collections;

public class Archer : EnemyRanged {

    public Archer (IUnit player)
    {
		PrepareTime = 0.5f;
		Cooldown = 0.5f;
		TargetUnit = player;
        HealthMax = 10;
        HealthCurrent = 10;
        MovementSpeedBase = 0.7f;
		ActRange = 6;
		TargetDistanceMax = 6;
		TargetDistanceMin = 3;
        reviveTypeString = "Enemy_Archer";
        EnemyConstructor();
    }

    public override void Act(Vector2 pos)
    {
		new Arrow(ActValue, Rot, Pos, TargetUnit);
    }
}
