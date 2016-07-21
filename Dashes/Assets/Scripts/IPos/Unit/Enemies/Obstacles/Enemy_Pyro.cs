using UnityEngine;
using System.Collections;

public class Enemy_Pyro : EnemyRanged {

	float ActRate = 0.1f;
	float ActRateCurrent = 0f;
	float Accuracy = 0.5f;

	public Enemy_Pyro (IUnit player)
	{
		PrepareTime = 0.5f;
		Cooldown = 1.5f;
		TargetUnit = player;
		HealthMax = 10;
		HealthCurrent = 10;
		MovementSpeedBase = 0.7f;
		ActRange = 4;
		TargetDistanceMax = 4;
		TargetDistanceMin = 0;
		ActContinuousTime = 1.5f;
		reviveTypeString = "Enemy_Waller";

		MovementSpeedActPercent = 0.25f;

		ActValue = 0.5f;

		EnemyConstructor();
	}

	public override void Act(Vector2 pos)
	{
		if (ActRateCurrent <= 0) {
			new Flame (ActValue, Rot+Random.Range(-90,90)*(1-Accuracy), Pos, TargetUnit);
			ActRateCurrent = ActRate;
		} else {
			ActRateCurrent -= Time.deltaTime;
		}
	}
}
