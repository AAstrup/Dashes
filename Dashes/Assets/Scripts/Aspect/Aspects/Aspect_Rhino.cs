using UnityEngine;
using System.Collections;

public class Aspect_Rhino : Aspect
{

    public override void Activate(float triggervalue)
    {
        Player.FloatVars["MarkingDamage"].Add(0.10f);
    }

    public override void DeActivate()
    {
        Player.FloatVars["MarkingDamage"].Remove(0.10f);
    }

    public override void Init()
    {
        Activate(0f);
    }

    public Aspect_Rhino()
    {

        Passive = true;

        Title = "Rhino aspect";
        Description = "You dashes deal 10% of your damage.";
    }

}
