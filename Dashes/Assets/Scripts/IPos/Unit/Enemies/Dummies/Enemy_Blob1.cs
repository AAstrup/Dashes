using UnityEngine;
using System.Collections;

public class Enemy_Blob1 : EnemyMelee
{

    public Enemy_Blob1(IUnit player)
    {
        target = player;
        HealthMax = 14;
        HealthCurrent = 14;
        MovementSpeedBase = 1f;
        hitEffect = ParticleEffectHandler.particleType.effect_slashEffect;
        hitParticleMin = 1;
        attackChargeTime = 0.5f;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Blob1"]);
    }
}
