using UnityEngine;
using System.Collections;

public class Charger : EnemyMelee {

    public Charger(IUnit player)
    {
        target = player;
        HealthMax = 6;
        HealthCurrent = 6;
        MovementSpeedBase = 2f;
        moveAttackSpeedPercentage = MovementSpeedBase;
        continueFireTimeSeconds = 2f;
        cd = 1.5f;
        hitParticleASecond = 5f;
        hitParticleMin = 0;
        engageRange = 6f;
        attackChargeTime = 0.5f;
        hitEffect = ParticleEffectHandler.particleType.effect_slashEffect;
        reviveTypeString = "Enemy_Charger";
        FinishConstructor();
    }
    public override void Damage(float amount)
    {
        base.Damage(amount);
    }

    
}
