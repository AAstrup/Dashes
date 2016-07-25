using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Aspect_Tunicate : Aspect
{

    private float particleEmitDelay = 0f;

    private const float f = 0.2f;

    //private List<float> floats = new List<float>(); 

    public override void Activate(float triggervalue)
    {
        Active = true;
        Player.Effects.Add(new Effect(Player, Effect.EffectTypes.HealDelay, f*triggervalue, 3,
            ParticleEffectHandler.particleType.effect_flower, 1, 1));
        DeActivate();
    }

    public override void DeActivate()
    {
        Active = false;
    }

    public override void Init()
    {

    }

    public override void ActiveUpdate()
    {
        base.ActiveUpdate();
        if (particleEmitDelay <= 0f)
        {
            References.instance.particleHandler.Emit(ParticleEffectHandler.particleType.effect_redglow, 5, Player.Pos);
            particleEmitDelay = 0.25f;
        }
        else
        {
            particleEmitDelay -= Time.deltaTime;
        }
    }

    public Aspect_Tunicate()
    {
        Trigger = new AspectTrigger(AspectTrigger.AspectTriggerType.Damage, 1);
        Passive = false;

        Title = "Tunicate aspect";
        Description = "3 seconds after taking damage, you are healed for 20% of the damage recieved.";
    }

}
