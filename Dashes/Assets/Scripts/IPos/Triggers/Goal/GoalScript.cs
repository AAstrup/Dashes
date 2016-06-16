using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalScript : ITrigger {

	public GoalScript(Vector2 startPos, IUnit player)
    {
        Pos = startPos;
        lifeTimeSpan = 5;
        dmg = 1;
        triggerRange = 0.2f;
        radius = triggerRange;
        targets = new List<IUnit>() { player };
        gmjPrefabName = "Trigger_Goal";

        Init();
    }

    protected override void Trigger(IUnit victim)
    {
        References.instance.progressionHandler.MapComplete();
    }
}
