using UnityEngine;
using System.Collections;

public class Aspect_Horse : Aspect {

    public override void Activate(float triggervalue)
	 {
	     Player.MarkedUnitsCanAlsoReset = true;
	 }

    public override void DeActivate()
    {
        Player.MarkedUnitsCanAlsoReset = false;
    }

    public override void Init()
    {
        Activate(0f);
    }

    public Aspect_Horse()
    {
        
        Passive = true;

        Title = "Horse aspect";
        Description = "Dashing through marked foes also resets the cooldown of your dash.";
    }

}
