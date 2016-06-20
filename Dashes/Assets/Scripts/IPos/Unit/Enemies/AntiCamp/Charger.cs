using UnityEngine;
using System.Collections;

public class Charger : EnemyMelee {

    public Charger(IUnit player)
    {
        target = player;
        HealthMax = 20;
        HealthCurrent = 20;
        MovementSpeedBase = 2f;
        moveAttackSpeedPercentage = MovementSpeedBase;
        continueFireTimeSeconds = 2f;
        cd = 0.5f;
        hitParticleASecond = 100;
        engageRange = 4f;
        attackChargeTime = 0f;
        hitEffect = ParticleEffectHandler.particleType.effect_hit;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Charger"]);
    }
    public override void Damage(float amount)
    {
        base.Damage(amount);
    }

    
}
