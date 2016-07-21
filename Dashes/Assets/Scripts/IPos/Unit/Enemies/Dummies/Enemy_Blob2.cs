using UnityEngine;
using System.Collections;

public class Enemy_Blob2 : EnemyMelee
{

	public Enemy_Blob2(IUnit player)
    {
		TargetUnit = player;
        HealthMax = 14;
        HealthCurrent = 14;
        MovementSpeedBase = 1f;
        hitEffect = ParticleEffectHandler.particleType.effect_slashEffect;
        hitParticleMin = 1;
		PrepareTime = 0.5f;

		TargetDistanceMin = 5f;

        reviveTypeString = "Enemy_Blob2";
        EnemyConstructor();
    }

    public override void Die()
    {
        SpawnBlob(1);
        SpawnBlob(-1);
        base.Die();
    }

    void SpawnBlob(int dir)
    {
		var enemy = new Enemy_Blob1(TargetUnit);
        enemy.Pos = Pos + new Vector2(0.25f* dir,0f);
        References.instance.RoomHandler.UnitSpawned(enemy);
    }
}
