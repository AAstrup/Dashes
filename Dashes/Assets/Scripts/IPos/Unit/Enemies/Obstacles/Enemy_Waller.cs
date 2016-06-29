using UnityEngine;
using System.Collections;
using System;

public class Enemy_Waller : EnemyMelee {

	public Enemy_Waller(IUnit player)
    {
        target = player;
        HealthMax = 20;
        HealthCurrent = 20;
        MovementSpeedBase = 1.5f;
        continueFireTimeSeconds = 2f;
        cd = 1.5f;
        hitParticleASecond = 1.5f;
        engageRange = 6f;
        attackChargeTime = 0.5f;
        hitEffect = ParticleEffectHandler.particleType.effect_hit;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Waller"]);
    }

    Vector2[] firePositions;
    Vector2 firePos1;
    Vector2 firePos2;
    Vector2 firePos3;
    bool hasSetFirePos = false;

    public override void Fire(Vector2 pos)
    {
        if (!hasSetFirePos)
            SetFirePos(pos);
        foreach (var p in firePositions)
        {
            if (Vector2.Distance(p, target.Pos) <= hitRange)//Pos+deltaPos
            {
                DamagePlayer(damage, target);
            }
        }
        base.CreateMultipleEffect(firePositions);
    }

    public override void StartCoolingDown()
    {
        hasSetFirePos = false;
        base.StartCoolingDown();
    }

    private void SetFirePos(Vector2 pos)
    {
        hasSetFirePos = true;
        Vector2 lookVector = Vector3.Normalize(Pos - pos);
        Vector2 normalVector = new Vector2(lookVector.y, -lookVector.x);
        firePos1 = pos + normalVector * 0.5f;

        firePos2 = pos;

        firePos3 = pos - normalVector * 0.5f;

        firePositions = new Vector2[] { firePos1, firePos2, firePos3 };
    }
}
