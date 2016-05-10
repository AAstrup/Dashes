using UnityEngine;
using System.Collections;

public class Aspect_Bat : Aspect {

    public override void Activate(float triggervalue)
    {
        Player.Heal(Player.HealthMax*Player.Marked.Count*0.015f);
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

    public Aspect_Bat()
    {
        Trigger = new AspectTrigger(AspectTrigger.AspectTriggerType.Finisher, 1);
        Passive = false;

        Title = "Bat aspect";
        Description = "Foreach marked enemy upon finishing your combo, restore 1.5% of total health.";
    }

}
