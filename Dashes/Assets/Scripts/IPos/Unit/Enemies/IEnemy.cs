using UnityEngine;
using System.Collections;

public class IEnemy : IUnit {

	//TARGET
	public Vector2 TargetPos = Vector2.zero;
	public IUnit TargetUnit;

	//ACT
	public float Cooldown = 0.5f;//CD between finishing an action and starting the next
	float CooldownCurrent = 0f;
	public float PrepareTime = 0f;
	float PrepareTimeCurrent = 0f;
	public float ActValue = 1f; //Only used with DamageTarget method//
	public float ActRange = 1f; //The "attack" range of the unit
	public float ActHitRange = 1f; //The maximum distance for this unit and its target unit for the act to be succesful after the preparation
	public float ActContinuousTime = 0f;
	float ActContinuousTimeCurrent = 0f;
	public bool ActWhileFleeing = false;
	public bool RotateWhileActing = false;

	//MOVE
	public float TargetDistanceMin = 0f; //The distance this units seeks to have with its TargetUnit
	public float TargetDistanceMax = 0f;
	public bool Engage = true;
	public bool Flee = true;
	public float MovementSpeedActPercent = 0f; //The percent of the current movement speed (found in IUnit) this unit moves, while its acting

	//STATE
	enum State { Move, Prepare, Act, Cooldown, Stunned }
	State state = State.Move;

	public override void RoomStart ()
	{
		base.RoomStart ();
	}

	void Move(float movementspeed)
	{
		Pos += GetRotVector() * GetDir() * movementspeed * Time.deltaTime;
		SetVisualColors(new Color(1f,1f,0),"Search");
	}

	Vector2 GetRotVector()
	{
		return new Vector2 (Mathf.Cos (Rot * Mathf.Deg2Rad), Mathf.Sin (Rot * Mathf.Deg2Rad));
	}

	float GetDir()
	{
		if (TargetUnit == null && Engage) {
			return 1f; //ENGAGE if there is no target - this makes sure that the unit will move forward
						//If you wish for it to stand still, simply set its movement speed to 0
		}
		if (Vector2.Distance (Pos, TargetUnit.Pos) > TargetDistanceMax && Engage) {
			return 1f; //ENGAGE
		}
		else if (Vector2.Distance (Pos, TargetUnit.Pos) < TargetDistanceMin && Flee) {
			return -1f; //FLEE
		}
		return 0f; //NO MOVEMENT
	}

	private void OverlappingFix()
	{
		for (int i = 0; i < References.instance.RoomHandler.aliveEnemies.Count; i++)
		{
			if (References.instance.RoomHandler.aliveEnemies[i] == this)
				continue;
			if ((radius + References.instance.RoomHandler.aliveEnemies[i].radius) > Vector2.Distance(this.Pos, References.instance.RoomHandler.aliveEnemies[i].Pos))
			{
				var vector = (Pos - References.instance.RoomHandler.aliveEnemies[i].Pos);
				Pos = Pos + vector/48;
				References.instance.RoomHandler.aliveEnemies[i].Pos = References.instance.RoomHandler.aliveEnemies[i].Pos - vector/48;
				return;
			}
		}
	}

	protected virtual void StartPreparation(Vector2 targetpos)
	{
		SetAngle(targetpos);
		state = State.Prepare;
		PrepareTimeCurrent = PrepareTime;
		SetVisualColors(new Color(1f, 0.5f, 0f),"Starting preparation");
	}

	protected void StartAct()
	{
		Act(Pos + GetRotVector() * ActHitRange);
		SetVisualColors(new Color(1f, 0, 0), "Starting fire");

		if (ActContinuousTime > 0)
		{
			ActContinuousTimeCurrent = ActContinuousTime;
			state = State.Act;
		}
		else
		{
			StartCoolingDown();
		}
	}

	public virtual void Act(Vector2 pos) { }

	public virtual void StartCoolingDown()
	{
		state = State.Cooldown;
		CooldownCurrent = Cooldown;
		SetVisualColors(new Color(1f, 1f, 1f),"Starting CD");
	}

	public virtual void StartMove()
	{
		state = State.Move;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
		OverlappingFix();

		//Debug.Log (GetType().ToString ());
		UpdateTarget ();

		if (state == State.Stunned)
		{
			//Handled in IUnit
		}
		else if(state == State.Prepare) //Still preparating
		{
			PrepareTimeCurrent -= Time.deltaTime;
			if (PrepareTimeCurrent < 0)//Finished preparation, start attacking
				StartAct();
		}
		else if(state == State.Act) //Continue attacking
		{
			Move(MovementSpeedActPercent);
			if (RotateWhileActing) {
				SetAngle(TargetPos);
			}
			Act(Pos + GetRotVector()*ActHitRange);
			if (ActContinuousTimeCurrent > 0) {//Done attacking
				ActContinuousTimeCurrent -= Time.deltaTime;
			} else {
				StartCoolingDown ();
			}
		}
		else if(state == State.Cooldown)//if (CanFire())//Start attacking
		{
			CooldownCurrent -= Time.deltaTime;
			if (CooldownCurrent < 0)
				StartMove ();
		}
		else if(state == State.Move)
		{
			if (TargetUnit != null) {
				if (Vector2.Distance (Pos, TargetUnit.Pos) < ActRange && (GetDir() != -1 || ActWhileFleeing)) {
					StartPreparation (TargetUnit.Pos);
				}
				else
				{
					Move(MovementSpeedCurrent);
					SetAngle(TargetPos);
				}
			} 
			else 
			{
				Move(MovementSpeedCurrent);
				SetAngle(TargetPos);
			}
		}
	}


	public override void SetStunned(bool v)
	{
		if (v)
		{
			state = State.Stunned;
			SetVisualColors(new Color(1f, 1f, 1f), "Stunned");
		}
		else
		{
			state = State.Move;
		}
		base.SetStunned(v);
	}

	public void DamageTarget(float damageamount)
	{
		TargetUnit.Damage(damageamount);
	}

	public override void Die()
	{
		References.instance.RoomHandler.UnitDied(this);
		base.Die();
	}

	void SetAngle (Vector3 otherpos) {
		//float currentRot = transform.eulerAngles.z;
		var targetRot = Mathf.Atan2(otherpos.y - Pos.y, otherpos.x - Pos.x) * 180 / Mathf.PI;
		Rot = targetRot;
	}

	protected void EnemyConstructor()
	{
		GenericConstructor(References.instance.PrefabLibrary.Prefabs[reviveTypeString]);
		UpdateTarget ();
	}

	public void UpdateTarget()
	{
		if (TargetUnit != null) {
			TargetPos = TargetUnit.Pos;
		}
	}
}
