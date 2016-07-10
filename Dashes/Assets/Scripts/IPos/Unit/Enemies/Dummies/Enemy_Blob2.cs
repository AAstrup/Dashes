using UnityEngine;
using System.Collections;

public class Enemy_Blob2 : EnemyMelee
{

	public Enemy_Blob2(IUnit player)
    {
        target = player;
        HealthMax = 14;
        HealthCurrent = 14;
        MovementSpeedBase = 1f;
        hitEffect = ParticleEffectHandler.particleType.effect_slashEffect;
        hitParticleMin = 1;
        attackChargeTime = 0.5f;

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Blob2"]);
    }

    public override void Die()
    {
        SpawnBlob(1);
        SpawnBlob(-1);
        base.Die();
    }

    void SpawnBlob(int dir)
    {
        var enemy = new Enemy_Blob1(target);
        enemy.Pos = Pos + new Vector2(0.25f* dir,0f);
        References.instance.RoomHandler.UnitSpawned(enemy);
    }
}
