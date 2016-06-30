using UnityEngine;
using System.Collections;

public class Stupid : EnemyMelee {

    public Stupid(IUnit player)
    {
        target = player;
        HealthMax = 20;
        HealthCurrent = 20;
        MovementSpeedBase = 1f;
        hitEffect = ParticleEffectHandler.particleType.effect_slashEffect;
        hitParticleMin = 1;
        attackChargeTime = 0.5f;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Stupid"]);
    }
}
