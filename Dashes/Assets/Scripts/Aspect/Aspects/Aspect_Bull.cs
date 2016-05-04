using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class Aspect_Bull : Aspect {

    private float particleEmitDelay = 0f;

    private const float f = 0.71f;

    //private List<float> floats = new List<float>(); 

	public override void Activate()
    {
        Player.FloatVars["DashingDurationFirstIncrease"].Add(f);
	    Player.FloatVars["DashingSpeedIncrease"].Add(f); 
        Active = true;
    }

    public override void DeActivate()
    {
        Debug.Log("adw");
        Player.FloatVars["DashingDurationFirstIncrease"].Remove(f);
        Player.FloatVars["DashingSpeedIncrease"].Remove(f);
        Active = false;
        Debug.Log("aWDWD");
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

    public Aspect_Bull()
    {
        Trigger = new AspectTrigger(AspectTrigger.AspectTriggerType.StandTime,1);
        Passive = false;

        Title = "Bull aspect";
        Description = "After standing still for 1 second the duration and speed of your next dash is increased by 75%.";
    }

}
