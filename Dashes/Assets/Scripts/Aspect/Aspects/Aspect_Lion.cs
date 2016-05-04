using UnityEngine;
using System.Collections;

public class Aspect_Lion : Aspect {

    public override void Activate()
    {
        Player.Effects.Add(new Effect(Player,Effect.EffectTypes.Invulnerability,0,3f,ParticleEffectHandler.particleType.effect_halo, 0.2f,1));
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

    public Aspect_Lion()
    {
        Trigger = new AspectTrigger(AspectTrigger.AspectTriggerType.Finisher,4);
        Passive = false;

        Title = "Lion aspect";
        Description = "Finishing your combo with atleast 4 marked foes grants you invulnerability for 3 seconds.";
    }

}
