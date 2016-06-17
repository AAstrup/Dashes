using UnityEngine;
using System.Collections;

public class Enemy_Boss : IUnit {
    
    public Enemy_Boss()
    {
        HealthMax = 20;
        HealthCurrent = 20;
        MovementSpeedBase = 1f;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Stupid"]);
    }

    public override void Update()
    {
        Pos += new Vector2(1f , 1f) * Time.deltaTime;
        base.Update();
    }

    public override void Die()
    {
        new GoalScript(Pos, References.instance.UnitHandler.playerIUnit);
        base.Die();
    }
}
