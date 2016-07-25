using UnityEngine;
using System.Collections;

public class Archer : EnemyRanged {

    public Archer (IUnit player)
    {
        attackChargeTime = 0.5f;
        cd = 0.5f;
        target = player;
        HealthMax = 10;
        HealthCurrent = 10;
        MovementSpeedBase = 0.7f;
        hitRange = 6;
        engageRange = hitRange;
        fleeRange = 3;
        reviveTypeString = "Enemy_Archer";
        FinishConstructor();
    }

    public override void Fire(Vector2 pos)
    {
        new Arrow(damage, Rot, Pos, target);
    }
}
