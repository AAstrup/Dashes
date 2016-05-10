using UnityEngine;
using System.Collections;

public class Aspect_Spider : Aspect {

    public override void Activate(float triggervalue)
    {
        Player.Marked.ForEach(typ => typ.Effects.Add(new Effect(typ, Effect.EffectTypes.Slow, 0.80f, 3f,ParticleEffectHandler.particleType.effect_whiteglow, 0.25f,5)));
        Active = true;
        DeActivate();
    }

    public override void DeActivate()
    {
        Active = false;
    }

    public override void Init()
    {

    }

    public Aspect_Spider()
    {
        Trigger = new AspectTrigger(AspectTrigger.AspectTriggerType.Finisher,1);
        Passive = false;

        Title = "Spider aspect";
        Description = "Upon finishing your combo all marked units are slowed by 80% for 2 seconds.";
    }

}
