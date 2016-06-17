using UnityEngine;
using System.Collections;

public class Charger : EnemyMeleeStupid {

    public Charger(IUnit player)
    {
        target = player;
        HealthMax = 20;
        HealthCurrent = 20;
        MovementSpeedBase = 2f;
        moveAttackSpeedPercentage = MovementSpeedBase;
        fireTime = 2f;
        cd = 4f;
        hitParticleASecond = 100;
        startAttackRange = 4f;
        attackChannelingTime = 0f;
        hitEffect = ParticleEffectHandler.particleType.effect_hit;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Charger"]);
    }
    public override void Damage(float amount)
    {
        base.Damage(amount);
    }
}
