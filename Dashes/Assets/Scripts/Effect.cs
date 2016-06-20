using UnityEditor;
using UnityEngine;
using System.Collections;

public class Effect
{

    public EffectTypes EffectType;
    public float Value;
    public float LifeTime;
    public ParticleEffectHandler.particleType ParticleType = ParticleEffectHandler.particleType.effect_none;
    public float ParticleEmitFrequency;
    public int ParticlesEmitAmount;

    private float _lifeTtimeTotal;
    private float _particleEmitDelay = 0f;

    private IUnit _unit;

    public void Create(IUnit unit)
    {
        switch (EffectType)
        {
            case EffectTypes.Slow:
                unit.Slow = Mathf.Max(unit.Slow, Value);
                unit.UpdateMovementSpeed();
                break;
            case EffectTypes.Boost:
                unit.Boost = Mathf.Max(unit.Boost, Value);
                unit.UpdateMovementSpeed();
                break;
            case EffectTypes.Stun:
                unit.SetStunned(true);
                break;
            case EffectTypes.Invulnerability:
                unit.Invulnerable = true;
                break;
        }
            
    }

    public void Update(IUnit unit)
    {

        LifeTime -= Time.deltaTime;

        switch (EffectType)
        {
            case EffectTypes.DamageConstant:
                unit.Damage(Time.deltaTime*Value/_lifeTtimeTotal);
                break;
            case EffectTypes.HealConstant:
                unit.Heal(Time.deltaTime * Value / _lifeTtimeTotal);
                break;
        }

        if (ParticleType != ParticleEffectHandler.particleType.effect_none)
        {
            if (_particleEmitDelay <= 0)
            {
                _particleEmitDelay = ParticleEmitFrequency;
                References.instance.particleHandler.Emit(ParticleType, ParticlesEmitAmount, _unit.Pos);
            }
            else
            {
                _particleEmitDelay -= Time.deltaTime;
            }
        }

        if (LifeTime <= 0) { Die(unit); }
    }

    public void Die(IUnit unit)
    {
        unit.Effects.Remove(this);
        switch (EffectType)
        {
            case EffectTypes.Slow:
                if (!unit.Effects.Exists(typ => typ.EffectType == EffectTypes.Slow)) { unit.Slow = 0f; unit.UpdateMovementSpeed(); }
                break;
            case EffectTypes.Boost:
                if (!unit.Effects.Exists(typ => typ.EffectType == EffectTypes.Boost)) { unit.Boost = 0f; unit.UpdateMovementSpeed(); }
                break;
            case EffectTypes.Stun:
                if (!unit.Effects.Exists(typ => typ.EffectType == EffectTypes.Stun)) { unit.SetStunned(false); }
                break;
            case EffectTypes.DamageDelay:
                unit.Damage(Value);
                break;
            case EffectTypes.HealDelay:
                unit.Heal(Value);
                break;
            case EffectTypes.Invulnerability:
                if (!unit.Effects.Exists(typ => typ.EffectType == EffectTypes.Invulnerability)) { unit.Invulnerable = false; }
                break;
        }
    }

    public enum EffectTypes
    {
        Slow,
        Stun,
        Boost,
        DamageConstant,
        DamageDelay,
        Invulnerability,
        HealConstant,
        HealDelay
    }

    public Effect(IUnit unit,EffectTypes effectType,float value,float lifetime)
    {
        EffectType = effectType;
        Value = value;
        LifeTime = lifetime;
        _lifeTtimeTotal = lifetime;
        Create(unit);
    }

    public Effect(IUnit unit, EffectTypes effectType, float value, float lifetime, ParticleEffectHandler.particleType particleType, float particleEmitFrequency, int particleEmitAmount)
    {
        EffectType = effectType;
        Value = value;
        LifeTime = lifetime;
        _lifeTtimeTotal = lifetime;
        ParticleType = particleType;
        ParticleEmitFrequency = particleEmitFrequency;
        ParticlesEmitAmount = particleEmitAmount;
        _unit = unit;
        Create(_unit);
    }


}
