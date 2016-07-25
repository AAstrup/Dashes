using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats {

    List<int> xpRequirements = new List<int>() { 100,150,200,250,300 };
    int level = 1;
    int xp;

    public void Init()
    {

    }

    public void GainXP(int amount)
    {
        if(amount + xp >= xpRequirements[level])
        {
            xp = (amount + xp) - xpRequirements[level];
            References.instance.UIHandler.PresentNewAspect();
            level++;
        }
    }

}
