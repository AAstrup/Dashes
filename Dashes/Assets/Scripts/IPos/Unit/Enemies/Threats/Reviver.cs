using UnityEngine;
using System.Collections;
using System;

public class Reviver : EnemyMelee {

    public Reviver()
    {
        GetTarget();
        HealthMax = 14;
        HealthCurrent = 14;
        MovementSpeedBase = 1f;
        hitEffect = ParticleEffectHandler.particleType.effect_slashEffect;
        hitParticleMin = 1;
        attackChargeTime = 0.5f;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Reviver"]);
    }

    private void GetTarget()
    {
        if(References.instance.UnitHandler.DeadUnitsInRoom.Count > 0)
            target = References.instance.UnitHandler.DeadUnitsInRoom[0];
    }
}
