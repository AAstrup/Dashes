using UnityEngine;
using System.Collections;

public class Aspect_Tempest : Aspect {

    public override void Activate()
    {
        Player.FloatVars["DashingDurationIncrease"].Add(0.41f);
    }
    public override void DeActivate()
    {
        Player.FloatVars["DashingDurationIncrease"].Remove(0.41f);
    }
    public override void Init()
    {
        Activate();
    }

    public Aspect_Tempest()
    {
        
        Passive = true;

        Title = "Tempest aspect";
        Description = "The duration of your dashes are increased by 40%.";
    }

}
