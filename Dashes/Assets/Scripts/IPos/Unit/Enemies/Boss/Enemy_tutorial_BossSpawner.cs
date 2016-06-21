using UnityEngine;
using System.Collections;

public class Enemy_tutorial_BossSpawner : EnemyRanged {


	public Enemy_tutorial_BossSpawner(IUnit player)
    {
        target = player;
        attackChargeTime = 0f;
        cd = 0.05f;
        HealthMax = 250;
        HealthCurrent = 250;

        MovementSpeedBase = 3f;
        MovementSpeedCurrent = MovementSpeedBase;
        hitRange = 6;
        engageRange = hitRange;
        fleeRange = 3;
        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_tutorial_BossSpawner"]);
    }

    protected override void CollisionEvent(Collision col)
    {
        if (col.collided)
        {
            var helpUnit = new Enemy_tutorial_Still(target);
            helpUnit.Pos = Pos - new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            Pos = References.instance.RoomHandler.GetCurrentRoom().GetWorldPos();
        }
    }
}
