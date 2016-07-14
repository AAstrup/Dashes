using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemy_medic : EnemyFlee {

	List<IUnit> Targets;
	List<GameObject> TargetBeams;
	int TargetNo;

	public Enemy_medic (IUnit player)
	{
		
		Targets = new List<IUnit> ();
		TargetBeams = new List<GameObject> ();

		/*attackChargeTime = 0.0f;
		cd = 0.0f;
		target = player;
		MovementSpeedBase = 1.2f;
		hitRange = 6;
		engageRange = hitRange;
		fleeRange = 5;*/

		HealthMax = 10;
		HealthCurrent = 10;

		TargetNo = 2;

		GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Archer"]);

	}

	public override void Update ()
	{
		UpdateTargetBeams ();
		base.Update ();
	}

	public override void RoomStart()
	{
		List<IUnit> temp = new List<IUnit> ();

		References.instance.UnitHandler.Units.ForEach (typ => 
			{
				if (typ.GetType () != typeof(Enemy_medic) && typ.GetType () != typeof(PlayerController) && !typ.Invulnerable) 
				{
					temp.Add (typ);
				}
			}
		);

		temp.OrderBy(typ => Vector2.Distance(References.instance.UnitHandler.playerController.Pos,typ.Pos));

		for (int i = 0; i < Mathf.Min(TargetNo,temp.Count); i++) {

			if (temp.Count > i) {
				Targets.Add (temp [i]);
			}

		}

		Targets.ForEach (typ => typ.Invulnerable = true);

		CreateTargetBeams ();
		UpdateTargetBeams ();
	}

	public override void Die ()
	{
		Targets.ForEach (typ => typ.Invulnerable = false);
		TargetBeams.ForEach (typ => References.instance.DestroyGameObject (typ));
		base.Die ();
	}

	public void UpdateTargetBeams()
	{
		for (int i = 0; i < Targets.Count; i++) {
			TargetBeams [i].transform.localScale = new Vector3 (Vector2.Distance(Pos,Targets[i].Pos),0.5f,1);
			TargetBeams [i].transform.position = (Pos + Targets [i].Pos) / 2f;
			TargetBeams [i].transform.rotation = Quaternion.EulerAngles (0,0,GetAngle(Targets[i].Pos)*Mathf.Deg2Rad);
		}
	}

	public void CreateTargetBeams()
	{
		for (int i = 0; i < Targets.Count; i++) {
			TargetBeams.Add (References.instance.CreateGameObject (References.instance.PrefabLibrary.Prefabs ["MedicTargetBeam"]));
		}
	}

	float GetAngle(Vector3 targetpos)
	{
		return Mathf.Atan2(targetpos.y - Pos.y, targetpos.x - Pos.x) * 180 / Mathf.PI;
	}

}
