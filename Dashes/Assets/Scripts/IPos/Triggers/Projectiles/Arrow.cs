using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arrow : ITrigger {

    public Arrow(float rotation,Vector2 startPos,IUnit player)
    {
        Rot = rotation;
        Pos = startPos;
        lifeTimeSpan = 5;
        dmg = 1;
        triggerRange = 0.2f;
        radius = triggerRange;
        gmjPrefabName = "Arrow";
        movementSpeed = 5f;
        targets = new List<IUnit> (){ player};
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
