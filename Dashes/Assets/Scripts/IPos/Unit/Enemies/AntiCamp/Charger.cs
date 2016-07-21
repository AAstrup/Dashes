using UnityEngine;
using System.Collections;

public class Charger : EnemyMelee {

    public Charger(IUnit player)
    {
		TargetUnit = player;
        HealthMax = 6;
        HealthCurrent = 6;
        MovementSpeedBase = 2f;
		MovementSpeedActPercent = MovementSpeedBase;
		ActContinuousTime = 2f;
		Cooldown = 1.5f;
        hitParticleASecond = 5f;
        hitParticleMin = 0;
		TargetDistanceMax = 6f;
		PrepareTime = 0.5f;
        hitEffect = ParticleEffectHandler.particleType.effect_slashEffect;
        reviveTypeString = "Enemy_Charger";
        EnemyConstructor();
    }
    public override void Damage(float amount)
    {
        base.Damage(amount);
    }

    
}
