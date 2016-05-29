using UnityEngine;
using System.Collections;

public class Aspect_Frog : Aspect {
    public override void Activate(float triggervalue)
    {
        Player.FloatVars["DashingDurationFirstIncrease"].Add(1.01f);
    }

    public override void DeActivate()
    {
        Player.FloatVars["DashingDurationFirstIncrease"].Remove(1.01f);
    }

    public override void Init()
    {
        Activate(0f);
    }

    public Aspect_Frog()
    {
        
        Passive = true;

        Title = "Frog aspect";
        Description = "The duration of your dashes are increased by 100%, while you have no combo.";
    }

}
