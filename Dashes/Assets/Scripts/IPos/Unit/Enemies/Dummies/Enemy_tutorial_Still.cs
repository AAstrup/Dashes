using UnityEngine;
using System.Collections;

public class Enemy_tutorial_Still : IEnemy {

	public Enemy_tutorial_Still(IUnit player)
    {
        HealthMax = 10;
        HealthCurrent = 10;
        MovementSpeedBase = 0f;
        MovementSpeedCurrent = MovementSpeedBase;
		ActValue = 0f;
        reviveTypeString = "Enemy_tutorial_Still";
        EnemyConstructor();
    }
}
