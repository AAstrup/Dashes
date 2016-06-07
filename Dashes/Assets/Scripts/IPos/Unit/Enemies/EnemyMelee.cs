using UnityEngine;
using System.Collections;

public class EnemyMeleeStupid : AINavigation {
    
    public IUnit target;
    protected ParticleEffectHandler.particleType hitEffect = ParticleEffectHandler.particleType.effect_none;
    protected int hitParticleASecond;//particles per second
    protected int hitParticleMin;//minimum particles emitted.
    
    public float damage = 1f;

    public override void Update()
    {
        base.Update();
        Update(target);
    }

    public override void Fire(Vector2 pos)
    {
        if (Vector2.Distance(Pos+deltaPos, target.Pos) <= hitRange)
        {
            DamagePlayer(damage, target);
        }
        CreateEffect(pos);
    }

    void CreateEffect(Vector2 pos)
    {
        if(hitEffect != ParticleEffectHandler.particleType.effect_none)
            References.instance.particleHandler.Emit(hitEffect, Mathf.FloorToInt(hitParticleASecond * Time.deltaTime) + hitParticleMin, Pos + deltaPos);
    }
}
