using UnityEngine;
using System.Collections;

public class Charger : EnemyMelee {

    public Charger(IUnit player)
    {
        target = player;
        HealthMax = 10;
        HealthCurrent = 10;
        MovementSpeedBase = 2f;
        moveAttackSpeedPercentage = MovementSpeedBase;
        continueFireTimeSeconds = 2f;
        cd = 1.5f;
        hitParticleASecond = 100;
        engageRange = 6f;
        attackChargeTime = 0.5f;
        hitEffect = ParticleEffectHandler.particleType.effect_hit;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Charger"]);
    }
    public override void Damage(float amount)
    {
        base.Damage(amount);
    }

    
}
