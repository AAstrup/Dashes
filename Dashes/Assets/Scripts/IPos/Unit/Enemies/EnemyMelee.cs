using UnityEngine;
using System.Collections;

public class EnemyMelee : AINavigation {
    
    public IUnit target;
    protected ParticleEffectHandler.particleType hitEffect = ParticleEffectHandler.particleType.effect_none;
    protected float hitParticleASecond;//particles per second
    protected int hitParticleMin;//minimum particles emitted.
    protected float particlesToCreateSum;// when reaching 1 or above creates a particle or more, otherwise add up the amount until reaching 1 or above;

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
        CreateSingleEffect(pos);
    }

    protected void CreateSingleEffect(Vector2 pos)
    {
        particlesToCreateSum += hitParticleASecond * Time.deltaTime;
        CreateOnlyEffect(pos);
        particlesToCreateSum -= Mathf.FloorToInt(particlesToCreateSum);
    }

    protected void CreateMultipleEffect(Vector2[] pos)
    {
        particlesToCreateSum += hitParticleASecond * Time.deltaTime;
        foreach (var p in pos)
        {
            CreateOnlyEffect(p);
        }
        particlesToCreateSum -= Mathf.FloorToInt(particlesToCreateSum);
    }

    private void CreateOnlyEffect(Vector2 pos) {
        if (hitEffect != ParticleEffectHandler.particleType.effect_none && Mathf.FloorToInt(Mathf.Max(particlesToCreateSum,hitParticleMin)) != 0)
            References.instance.particleHandler.Emit(hitEffect, Mathf.FloorToInt(particlesToCreateSum) + hitParticleMin, pos, GBref.transform.eulerAngles.z);
    }
}
