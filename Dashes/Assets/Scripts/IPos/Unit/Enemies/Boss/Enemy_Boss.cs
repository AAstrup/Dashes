using UnityEngine;
using System.Collections;

public class Enemy_Boss : IUnit
{

    public float activeDelay = 0.25f;

    public float laserSpawnTimeBase = 0.05f;
    public float laserSpawnTimeCurrent = 0f;
    public float laserDir = 1f;
    public float laserDirChangeDelay = 0f;


    public Enemy_Boss()
    {
        HealthMax = 200;
        HealthCurrent = 200;
        MovementSpeedBase = 1f;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Stupid"]);
    }

    public override void Update()
    {
        base.Update();

        //if (Stunned)
        //    return;

        Pos = References.instance.RoomHandler.GetCurrentRoom().GetWorldPos();

        if (activeDelay > 0)
        {
            activeDelay -= Time.deltaTime;
            return;
        }


        //Pos += new Vector2(10,0);

        Rot += 65 * Time.deltaTime * laserDir;

        laserDirChangeDelay -= Time.deltaTime;
        if (laserDirChangeDelay <= 0)
        {
            laserDir *= -1;
            laserDirChangeDelay = 3f;
        }

        if (laserSpawnTimeCurrent <= 0)
        {
            for(int i=0;i<4;i++)
            {
                new Laser(0.15f, Rot+90*i, Pos, References.instance.UnitHandler.playerController);
            }
            laserSpawnTimeCurrent = laserSpawnTimeBase;
        }
        else
        {
            laserSpawnTimeCurrent -= Time.deltaTime;
        }
    }

    public override void Die()
    {
        new GoalScript(Pos, References.instance.UnitHandler.playerIUnit);
        base.Die();
    }

    void SetAngle(Vector3 otherpos)
    {
        var targetRot = Mathf.Atan2(otherpos.y - Pos.y, otherpos.x - Pos.x) * 180 / Mathf.PI;
        Rot = targetRot;
    }


    enum AttackType
    {
        LaserArms,

    }

}
