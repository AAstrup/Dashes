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
        hitParticleASecond = 50;
        engageRange = 6f;
        attackChargeTime = 0.5f;
        hitEffect = ParticleEffectHandler.particleType.effect_hit;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Waller"]);
    }

    Vector2 firePos1;
    Vector2 firePos2;
    Vector2 firePos3;
    bool hasSetFirePos = false;

    public override void Fire(Vector2 pos)
    {
        if (!hasSetFirePos)
            SetFirePos(pos);
        base.Fire(firePos1);
        base.Fire(firePos2);
        base.Fire(firePos3);
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
        Debug.Log("firePos1 " + firePos1.ToString());

        firePos2 = pos;
        Debug.Log("firePos2 " + firePos2.ToString());

        firePos3 = pos - normalVector * 0.5f;
        Debug.Log("firePos3 " + firePos3.ToString());

    }
}
