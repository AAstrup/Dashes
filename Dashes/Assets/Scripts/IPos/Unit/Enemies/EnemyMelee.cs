using UnityEngine;
using System.Collections;

public class EnemyMelee : AINavigation {
    
    public IUnit target;
    protected ParticleEffectHandler.particleType hitEffect = ParticleEffectHandler.particleType.effect_none;
    protected int hitParticleASecond;//particles per second
    protected int hitParticleMin;//minimum particles emitted.

    public override void Update()
    {
        base.Update();
        Update(target);
    }

    public override void Fire(Vector2 pos)
    {
        if (Vector2.Distance(pos, target.Pos) <= hitRange)//Pos+deltaPos
        {
            DamagePlayer(damage, target);
        }
        CreateEffect(pos);
    }

    void CreateEffect(Vector2 pos)
    {
        if(hitEffect != ParticleEffectHandler.particleType.effect_none)
            References.instance.particleHandler.Emit(hitEffect, Mathf.CeilToInt(hitParticleASecond * Time.deltaTime) + hitParticleMin, pos,GBref.transform.eulerAngles.z);//
    }
}
