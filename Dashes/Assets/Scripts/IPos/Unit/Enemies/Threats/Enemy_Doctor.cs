using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemy_Doctor : EnemyMelee {

	List<IUnit> Targets;
	List<GameObject> TargetBeams;
	bool PlayerFocus = false;

	public Enemy_Doctor (IUnit player)
	{
		TargetUnit = player; //In case der ingen friendlies er, som ikke allerede er invulvernable, så sættes target til spilleren

		Targets = new List<IUnit> ();
		TargetBeams = new List<GameObject> ();

		MovementSpeedBase = 1.4f;

		HealthMax = 10;
		HealthCurrent = 10;

		TargetDistanceMin = 7f;
		TargetDistanceMax = 7f;

		ActRange = 2f;
		hitEffect = ParticleEffectHandler.particleType.effect_slashEffect;

		PrepareTime = 0.5f;

		ActWhileFleeing = true;

		GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Archer"]);

	}

	public override void Update ()
	{
		if (TargetUnit.Invulnerable) {
			GetTarget ();
			StartMove ();
		}
		UpdateTargetBeams ();
		base.Update ();
	}

	public void GetTarget()
	{
		List<IUnit> temp = new List<IUnit> ();

		References.instance.UnitHandler.Units.ForEach (typ => 
			{
				if (typ.GetType () != typeof(Enemy_medic) && typ.GetType() != typeof(Enemy_Doctor) && typ.GetType () != typeof(PlayerController) && !typ.Invulnerable) 
				{
					temp.Add (typ);
				}
			}
		);

		if (temp.Count == 0) {
			TargetUnit = References.instance.UnitHandler.playerController;
			TargetDistanceMin = 7f;
			TargetDistanceMax = 7f;
			Engage = false;
			PlayerFocus = true;
			ActRange = 1f;
		} else {
			temp.OrderBy (typ => Vector2.Distance (References.instance.UnitHandler.playerController.Pos, typ.Pos));
			TargetUnit = temp [0];
			TargetDistanceMin = 0f;
			TargetDistanceMax = 2f;
			Engage = true;
			PlayerFocus = false;
			ActRange = 2f;
		}
	}

	public override void RoomStart()
	{
		base.RoomStart ();

		GetTarget ();

		UpdateTargetBeams ();
	}

	public override void Act (Vector2 pos)
	{
		if (PlayerFocus) {
			if (Vector2.Distance(pos, TargetUnit.Pos) <= ActHitRange)
			{
				DamageTarget(ActValue);
			}
			CreateSingleEffect(pos);
		} else {
			TargetUnit.Invulnerable = true;
			Targets.Add (TargetUnit);
			CreateTargetBeam ();
			GetTarget ();
		}
	}

	public override void Die ()
	{
		Targets.ForEach (typ => typ.Invulnerable = false);
		TargetBeams.ForEach (typ => References.instance.DestroyGameObject (typ));
		base.Die ();
	}

	public void UpdateTargetBeams()
	{
		for (int i = 0; i < TargetBeams.Count; i++) {
			TargetBeams [i].transform.localScale = new Vector3 (Vector2.Distance(Pos,Targets[i].Pos),0.3f,0.3f);
			TargetBeams [i].transform.position = (Pos + Targets [i].Pos) / 2f;
			TargetBeams [i].transform.rotation = Quaternion.EulerAngles (0,0,GetAngle(Targets[i].Pos)*Mathf.Deg2Rad);
		}
	}

	public void CreateTargetBeam()
	{
		TargetBeams.Add (References.instance.CreateGameObject (References.instance.PrefabLibrary.Prefabs ["MedicTargetBeam"]));
	}

	float GetAngle(Vector3 targetpos)
	{
		return Mathf.Atan2(targetpos.y - Pos.y, targetpos.x - Pos.x) * 180 / Mathf.PI;
	}

}
