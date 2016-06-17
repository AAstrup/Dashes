using UnityEngine;
using System.Collections;

public class Archer : EnemyRanged {

    public Archer (IUnit player)
    {
        cd = 2f;
        target = player;
        HealthMax = 20;
        HealthCurrent = 20;
        MovementSpeedBase = 0.7f;
        hitRange = 6;
        startAttackRange = hitRange;
        fleeRange = 3;
        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Archer"]);
    }
}
