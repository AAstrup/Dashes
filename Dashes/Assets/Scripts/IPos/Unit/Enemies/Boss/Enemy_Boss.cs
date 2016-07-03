using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Enemy_Boss : IUnit
{

    public float activeDelay = 0.25f;

    public float laserDelay = 0f;
    public float laserSpawnTime = 0f;
    public float laserDir = 1f;
    public float laserDirChangeDelay = 0f;

    private AttackType currentAttackType = AttackType.laserArms;
    private float AttackTime = 0f;
    private float AttackCooldown = 0f;

    private List<LaserBounce> laserBounces;
    private int lastBounceTeleportRoll = -1;

    private float damaged33 = 0f;
    private float damaged50 = 0f;
    private float damaged66 = 0f;

    public Enemy_Boss()
    {
        HealthMax = 120;
        HealthCurrent = 120;
        MovementSpeedBase = 1f;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Boss"]);

        laserBounces = new List<LaserBounce>();

        References.instance.UIHandler.EnableBoss();
        References.instance.UIHandler.UpdateBar("BossBar", HealthCurrent / HealthMax, true);
    }

    public override void Update()
    {
        base.Update();

        //if (Stunned)
        //    return;


        if (activeDelay > 0)
        {
            activeDelay -= Time.deltaTime;
            return;
        }


        if (AttackTime <= 0)
        {
            if (AttackCooldown <= 0)
            {
                switch (currentAttackType)
                {
                    case AttackType.none:
                        break;
                    case AttackType.laserArms:
                        currentAttackType = AttackType.bounce;
                        AttackCooldown = 2.25f;
                        laserDelay = 0.5f;
                        break;
                    case AttackType.bounce:
                        currentAttackType = AttackType.laserArms;
                        AttackCooldown = 2.25f;
                        References.instance.particleHandler.Emit(ParticleEffectHandler.particleType.effect_explosion, 10, Pos);
                        Pos = References.instance.RoomHandler.GetCurrentRoom().GetWorldPos();
                        References.instance.particleHandler.Emit(ParticleEffectHandler.particleType.effect_explosion, 10, Pos);
                        break;
                }
                AttackTime = 10f;
            }
            else
            {
                Rot = GetAngle(References.instance.UnitHandler.playerController.Pos);
                Pos -= (References.instance.UnitHandler.playerController.Pos - Pos).normalized*0.25f*Time.deltaTime;
                AttackCooldown -= Time.deltaTime;
            }
        }
        else
        {
            AttackTime -= Time.deltaTime;

            switch (currentAttackType)
            {
                case AttackType.none:
                    break;
                case AttackType.laserArms:

                        if (laserDelay <= 0)
                        { 
                            Rot += (40+15*damaged33) * Time.deltaTime * laserDir;

                            laserDirChangeDelay -= Time.deltaTime * damaged33;
                            if (laserDirChangeDelay <= 0)
                            {
                                laserDir *= -1;
                                laserDirChangeDelay = Random.Range(2f,3.5f);
                            }

                            if (laserSpawnTime <= 0)
                            {
                                for(int i=0;i<2+damaged33+damaged66;i++)
                                {
                                    new Laser(0.15f, Rot + (360 / (2 + damaged33 + damaged66))* i, Pos, References.instance.UnitHandler.playerController);
                                }
                                laserSpawnTime = 0.05f;
                            }
                            else
                            {
                                laserSpawnTime -= Time.deltaTime;
                            }
                        }
                        else
                        {
                            laserDelay -= Time.deltaTime;
                        }

                    break;

                case AttackType.bounce:


                        if (laserSpawnTime <= 0)
                        {
                            BounceRandomTeleport();
                            Rot = GetAngle(References.instance.UnitHandler.playerController.Pos);
                            var total = 1 + damaged50 + damaged33;
                            for(int i=0;i<total;i++)
                            {
                                float r = 0;
                                if (total == 1)
                                {
                                    r = Rot;
                                }
                                else if (total == 2)
                                {
                                    r = Rot - 15 + 30 * i;
                                }
                                else if (total == 3)
                                {
                                    r = Rot - 30 + 30 * i;
                                }
                                laserBounces.Add(new LaserBounce(1f, r, Pos, References.instance.UnitHandler.playerController));
                            }
                            laserSpawnTime = 1.5f + damaged33 * 0.5f + damaged50 * 0.5f;
                        }
                        else
                        {
                            Rot = GetAngle(References.instance.UnitHandler.playerController.Pos);
                            laserSpawnTime -= Time.deltaTime;
                        }

                    if (AttackTime <= 0)
                    {
                        laserBounces.ForEach(typ =>
                        {
                            References.instance.particleHandler.Emit(ParticleEffectHandler.particleType.effect_boss1_lasterBounce, 50, typ.Pos);
                            typ.Delete();
                        });
                        laserBounces = new List<LaserBounce>();
                    }

                    break;
            }

            
        }
        

    }

    public void BounceRandomTeleport()
    {
        References.instance.particleHandler.Emit(ParticleEffectHandler.particleType.effect_explosion, 10, Pos);
        var dice = Random.Range(0, 4);

        while (lastBounceTeleportRoll == dice)
        {
            dice = Random.Range(0, 4);
        }

        lastBounceTeleportRoll = dice;

        if (dice == 0)
        {
            Pos = References.instance.RoomHandler.GetCurrentRoom().GetWorldPos() +
                  new Vector2(-References.instance.RoomHandler.GetCurrentRoom().GetRoomWidth() /3,
                      -References.instance.RoomHandler.GetCurrentRoom().GetRoomHeight()/2);
        }
        if (dice == 1)
        {
            Pos = References.instance.RoomHandler.GetCurrentRoom().GetWorldPos() +
                  new Vector2(References.instance.RoomHandler.GetCurrentRoom().GetRoomWidth()/3,
                      -References.instance.RoomHandler.GetCurrentRoom().GetRoomHeight()/2);
        }
        if (dice == 2)
        {
            Pos = References.instance.RoomHandler.GetCurrentRoom().GetWorldPos() +
                  new Vector2(-References.instance.RoomHandler.GetCurrentRoom().GetRoomWidth()/3,
                      References.instance.RoomHandler.GetCurrentRoom().GetRoomHeight()/3);
        }
        if (dice == 3)
        {
            Pos = References.instance.RoomHandler.GetCurrentRoom().GetWorldPos() +
                  new Vector2(References.instance.RoomHandler.GetCurrentRoom().GetRoomWidth()/3,
                      References.instance.RoomHandler.GetCurrentRoom().GetRoomHeight()/3);
        }
        
    }

    public override void Die()
    {
        new GoalScript(Pos, References.instance.UnitHandler.playerIUnit);
        References.instance.UIHandler.DisableBoss();
        base.Die();
    }

    void SetAngle(Vector3 otherpos)
    {
        var targetRot = Mathf.Atan2(otherpos.y - Pos.y, otherpos.x - Pos.x) * 180 / Mathf.PI;
        Rot = targetRot;
    }

    float GetAngle(Vector3 targetpos)
    {
        return Mathf.Atan2(targetpos.y - Pos.y, targetpos.x - Pos.x) * 180 / Mathf.PI;
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);
        if (HealthCurrent/HealthMax <= 0.66f)
        {
            damaged33 = 1f;
        }
        if (HealthCurrent / HealthMax <= 0.5f)
        {
            damaged50 = 1f;
        }
        if (HealthCurrent / HealthMax <= 0.33f)
        {
            damaged66 = 1f;
        }
        References.instance.UIHandler.UpdateBar("BossBar",HealthCurrent/HealthMax,true);
    }

    enum AttackType
    {
        none,
        laserArms,
        bounce,
    }

}
