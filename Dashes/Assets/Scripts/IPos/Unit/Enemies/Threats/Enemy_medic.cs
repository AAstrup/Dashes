using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemy_medic : EnemyRanged {

	List<IUnit> Targets;

	public Enemy_medic (IUnit player)
	{
		Targets = new List<IUnit> ();
		attackChargeTime = 0.5f;
		cd = 0.5f;
		target = player;
		HealthMax = 10;
		HealthCurrent = 10;
		MovementSpeedBase = 0.7f;
		hitRange = 6;
		engageRange = hitRange;
		fleeRange = 3;
		GenericConstructor(References.instance.PrefabLibrary.Prefabs["Enemy_Archer"]);
	}

	public override void RoomStart()
	{
		List<IUnit> temp = new List<IUnit> ();

		References.instance.UnitHandler.Units.ForEach (typ => 
			{
				if (typ.GetType () != typeof(Enemy_medic) && typ.GetType () != typeof(PlayerController)) 
				{
					temp.Add (typ);
				}
			}
		);

		temp.OrderBy(typ => Vector2.Distance(References.instance.UnitHandler.playerController.Pos,typ.Pos));

		for (int i = 0; i < 2; i++) {

			if (temp.Count > i) {
				Targets.Add (temp [i]);
			}

		}

		Targets.ForEach (typ => typ.Invulnerable = true);

	}



	public override void Fire(Vector2 pos)
	{
		new Arrow(damage, Rot, Pos, target);
	}

	public override void Die ()
	{
		Targets.ForEach (typ => typ.Invulnerable = false);
		base.Die ();
	}

}
