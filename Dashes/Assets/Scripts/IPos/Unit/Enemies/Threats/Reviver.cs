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
        
        reviveTypeString = "Enemy_Reviver";
        FinishConstructor();
    }

    public override void Update()
    {
        if (target == null)
            GetTarget();
        base.Update();
    }

    public override void Fire(Vector2 pos)
    {
        if (target.HealthCurrent > 0)
        {
            target = null;
            return;
        }
        if (Vector2.Distance(pos, target.Pos) <= hitRange)//Pos+deltaPos
        {
            target.Revive();
        }
        CreateSingleEffect(pos);
    }

    private void GetTarget()
    {
        if(References.instance.UnitHandler.DeadUnitsInRoom.Count > 0)
            target = References.instance.UnitHandler.DeadUnitsInRoom[0];
    }
}
