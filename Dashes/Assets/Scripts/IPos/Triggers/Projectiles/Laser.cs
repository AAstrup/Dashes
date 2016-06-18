using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : ITrigger
{

    public Laser(float _dmg, float rotation, Vector2 startPos, IUnit player)
    {
        Rot = rotation;
        Pos = startPos;
        lifeTimeSpan = 5;
        dmg = _dmg;
        triggerRange = 0.2f;
        radius = triggerRange;
        gmjPrefabName = "Laser";
        movementSpeed = 5f;
        targets = new List<IUnit>() { player };
        effectTrigger = ParticleEffectHandler.particleType.effect_hit;
        effectTimespan = ParticleEffectHandler.particleType.effect_hit;

        Init();
    }

    protected override void Trigger(IUnit victim)
    {
        victim.Damage(dmg);
        base.Trigger(victim);
    }
}
