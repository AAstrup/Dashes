using UnityEngine;
using System.Collections;

public class Aspect_Snake : Aspect
{

    public override void Activate(float triggervalue)
    {
        Player.Marked[Player.Marked.Count-1].Effects.Add(new Effect(Player.Marked[Player.Marked.Count-1], Effect.EffectTypes.DamageConstant, References.instance.UnitHandler.playerController.AttackDamage * 0.4f, 4f, ParticleEffectHandler.particleType.effect_whiteglow, 0.25f, 5));
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

    public Aspect_Snake()
    {
        Trigger = new AspectTrigger(AspectTrigger.AspectTriggerType.Finisher, 1);
        Passive = false;

        Title = "Snake aspect";
        Description = "After finishing your combo the last marked enemy takes 40% of your damage over 4 seconds.";
    }

}
