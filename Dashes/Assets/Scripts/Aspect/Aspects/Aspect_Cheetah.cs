using UnityEngine;
using System.Collections;

public class Aspect_Cheetah : Aspect
{

    private float particleEmitDelay = 0f;

    public override void Activate()
    {
        Player.FloatVars["MovementSpeedIncrease"].Add(0.46f);
        Debug.Log("ACTIVATED");
        Active = true;
    }

    public override void DeActivate()
    {
        Player.FloatVars["MovementSpeedIncrease"].Remove(0.46f);
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
            References.instance.particleHandler.Emit(ParticleEffectHandler.particleType.effect_orangeglow, 1, Player.Pos);
            particleEmitDelay = 0.25f;
        }
        else
        {
            particleEmitDelay -= Time.deltaTime;
        }
    }

    public Aspect_Cheetah()
    {
        Trigger = new AspectTrigger(AspectTrigger.AspectTriggerType.Combo,3);
        Passive = false;

        Title = "Cheetah aspect";
        Description = "While having a combo of at least 3 your movement speed is increased by 45%.";
    }
}
