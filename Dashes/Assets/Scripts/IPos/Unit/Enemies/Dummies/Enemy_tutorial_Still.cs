using UnityEngine;
using System.Collections;

public class Enemy_tutorial_Still : AINavigation {

	public Enemy_tutorial_Still(IUnit player)
    {
        HealthMax = 10;
        HealthCurrent = 10;
        MovementSpeedBase = 0f;
        MovementSpeedCurrent = MovementSpeedBase;
        damage = 0f;
        reviveTypeString = "Enemy_tutorial_Still";
        FinishConstructor();
    }
}
