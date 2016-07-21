using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flame : ITrigger {

	public Flame(float _dmg,float rotation,Vector2 startPos,IUnit player)
	{
		Rot = rotation;
		Pos = startPos;
		lifeTimeSpan = 0.3f;
		dmg = _dmg;
		triggerRange = 0.2f;
		radius = triggerRange;
		gmjPrefabName = "Arrow";
		speed = new Vector2(Mathf.Cos(Rot*Mathf.Deg2Rad),Mathf.Sin(Rot*Mathf.Deg2Rad))*5f;
		targets = new List<IUnit> (){ player};
		effectTrigger = ParticleEffectHandler.particleType.effect_hit;
		effectTimespan = ParticleEffectHandler.particleType.effect_hit;

		Init();
	}

	protected override void Trigger(IUnit victim)
	{
		victim.Damage(dmg);
		base.Trigger(victim);
	}
}
