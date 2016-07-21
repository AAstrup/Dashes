using UnityEngine;
using System.Collections;
using System;

public class Enemy_Waller : EnemyMelee {

	public Enemy_Waller(IUnit player)
    {
		TargetUnit = player;
        HealthMax = 13;
        HealthCurrent = 13;
        MovementSpeedBase = 1.5f;
		PrepareTime = 2f;
		Cooldown = 1.5f;
        hitParticleASecond = 1.5f;
		TargetDistanceMin = 6f;
		TargetDistanceMax = 6f;
		Flee = false;
		ActContinuousTime = 0.5f;
        hitEffect = ParticleEffectHandler.particleType.effect_slashEffect;
        
        reviveTypeString = "Enemy_Waller";
		EnemyConstructor();
    }

    Vector2[] firePositions;
    Vector2 firePos1;
    Vector2 firePos2;
    Vector2 firePos3;
    bool hasSetFirePos = false;

    public override void Act(Vector2 pos)
    {
        if (!hasSetFirePos)
            SetFirePos(pos);
        foreach (var p in firePositions)
        {
			if (Vector2.Distance(p, TargetUnit.Pos) <= ActHitRange)//Pos+deltaPos
            {
				DamageTarget(ActValue);
            }
        }
        base.CreateMultipleEffect(firePositions);
    }

    public override void StartCoolingDown()
    {
        hasSetFirePos = false;
        base.StartCoolingDown();
    }

    private void SetFirePos(Vector2 pos)
    {
        hasSetFirePos = true;
        Vector2 lookVector = Vector3.Normalize(Pos - pos);
        Vector2 normalVector = new Vector2(lookVector.y, -lookVector.x);
        firePos1 = pos + normalVector * 0.5f;

        firePos2 = pos;

        firePos3 = pos - normalVector * 0.5f;

        firePositions = new Vector2[] { firePos1, firePos2, firePos3 };
    }
}
