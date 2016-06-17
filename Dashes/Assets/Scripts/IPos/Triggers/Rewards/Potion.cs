using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Potion : ITrigger {

    float _hpHealAmount;

    public void CreatePotion(string prefabName,float hpHealAmount,Vector2 startPos,IUnit player)
    {
        gmjPrefabName = prefabName;
        _hpHealAmount = hpHealAmount;
        triggerRange = 0.5f;
        Pos = startPos;
        lifeTimeSpan = 5;
        targets = new List<IUnit>() { player };

        Init();
    }

    protected override void Trigger(IUnit victim)
    {
        victim.Heal(_hpHealAmount);
        base.Trigger(victim);
    }

    protected override void TimeLeft()
    {
    }
}
