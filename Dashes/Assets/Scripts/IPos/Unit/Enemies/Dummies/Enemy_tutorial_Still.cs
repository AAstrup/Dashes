using UnityEngine;
using System.Collections;

public class Enemy_tutorial_Still : AINavigation {

	public Enemy_tutorial_Still(IUnit player)
    {
        HealthMax = 20;
        HealthCurrent = 20;
        MovementSpeedBase = 0f;
        MovementSpeedCurrent = MovementSpeedBase;
        damage = 0f;
        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_tutorial_Still"]);
    }
}
