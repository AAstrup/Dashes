using UnityEngine;
using System.Collections;
using System;

public class Reviver : EnemyMelee {

	public Reviver(IUnit player)
    {
        GetTarget();
        HealthMax = 14;
        HealthCurrent = 14;
        MovementSpeedBase = 1f;
        hitEffect = ParticleEffectHandler.particleType.effect_slashEffect;
        hitParticleMin = 1;
		PrepareTime = 0.5f;
        
        reviveTypeString = "Enemy_Reviver";
        EnemyConstructor();
    }

    public override void Update()
    {
		if (TargetUnit == null)
            GetTarget();
        base.Update();
    }

    public override void Act(Vector2 pos)
    {
		if (TargetUnit.HealthCurrent > 0)
        {
			TargetUnit = null;
            return;
        }
		if (Vector2.Distance(pos, TargetUnit.Pos) <= ActHitRange)//Pos+deltaPos
        {
			TargetUnit.Revive();
        }
        CreateSingleEffect(pos);
    }

    private void GetTarget()
    {
        if(References.instance.UnitHandler.DeadUnitsInRoom.Count > 0)
			TargetUnit = References.instance.UnitHandler.DeadUnitsInRoom[0];
    }
}
