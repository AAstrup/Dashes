using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class PlayerController : IUnit
{
    private Vector2 _facedir = Vector2.zero;
    private Vector2 _mousefacing = Vector2.zero;
    private Vector2 _speed = Vector2.zero;

    private bool _dashing;
    private const float _dashingSpeedBase = 15f;
    private float _dashingSpeedCurrent = 0f;
    private const float DashingDurationBase = 0.15f;
    private float DashingDurationCurrent;
    private float _dashingTime;

    private float _dashingCooldown;
    private const float DashingCooldownDuration = 2f;

    public int Combo = 0;
    public List<IUnit> Marked = new List<IUnit>();
    private const float _markDamage = 10f;

    private List<MarkedProjectile> _markedProjectiles;
    private const float _markedProjectileSpeed = 17.5f;

    private float _standingtime = 0f;
    private float _movetime = 0f;
    private float _standmovetimetriggerdelaybase = 1f;
    private float _standmovetimetriggerdelay = 0f;

    public bool MarkedUnitsCanAlsoReset = false;

    public Dictionary<string, List<float>> FloatVars = new Dictionary<string, List<float>>()
    {
        {"DashingDurationIncrease",new List<float>()},
        {"DashingDurationFirstIncrease",new List<float>()},
        {"DashingSpeedIncrease",new List<float>()},
        {"MovementSpeedIncrease",new List<float>()}
    };

    public float ListVal(List<float> list)
    {
        var t = 0f;
        list.ForEach(typ => t += typ);
        return t;
    }
    public float ListVal(string floatvarsname)
    {
        return ListVal(FloatVars[floatvarsname]);
    }

    public override void Update()
    {
        _markedProjectiles.ForEach(typ =>
        {
            typ.Update();
            if (typ.Lifetime <= 0)
            {
                References.instance.DestroyGameObject(typ.Body);
                _markedProjectiles.Remove(typ);
            }
        });

        if (!Stunned)
        {
            Control();
        }

        if (References.instance.colSystem.CollidesWithWall(this).collided)
        {
            _dashingTime = 0f;
        }
        base.Update();
    }

    public void UpdateCombo(int combono)
    {
        Combo = combono;
        References.instance.AspectHandler.UpdateTrigger(AspectTrigger.AspectTriggerType.Combo, Combo);
    }

    public void ResetCombo()
    {
        Combo = 0;
        References.instance.AspectHandler.UpdateTrigger(AspectTrigger.AspectTriggerType.Combo, Combo);
    }

    public void Control()
    {
        /*TEMP*/
        if (Input.GetKeyDown(KeyCode.M))
        {
            Effects.Add(new Effect(this,Effect.EffectTypes.Boost,0.5f,2f));
        }

        /*DASHING*/
        if (_dashing)
        {
            _speed = _mousefacing * _dashingSpeedCurrent;
            _dashingTime -= Time.deltaTime;
            if (_dashingTime <= 0)
            {
                _dashing = false;
            }

            
        }
        else if (References.instance.PlayerInput.Buttons["Attack"].Pressing &&
            !References.instance.PlayerInput.Buttons["Attack"].Pressed && _dashingCooldown <= 0)
        {
            _dashing = true;
            DashingDurationCurrent = DashingDurationBase * (1 + ListVal("DashingDurationIncrease")) * (Marked.Count == 0 ? (1 + ListVal("DashingDurationFirstIncrease")) : 1);
            _dashingTime = DashingDurationCurrent;
            _dashingCooldown = DashingCooldownDuration;
            _dashingSpeedCurrent = _dashingSpeedBase*(1+ListVal("DashingSpeedIncrease"));

            /*Nedenstående skal flyttes over i collision klassen, eller måske ikke... :/ */
            References.instance.UnitHandler.Units.ForEach(typ =>
            {
                var t = Vector3.Cross(_mousefacing, typ.Pos - Pos).magnitude;

                if (t < typ.Scale.x / 2 + Scale.x / 2 - 0.5f && Vector2.Distance(typ.Pos, Pos) < _dashingSpeedCurrent * DashingDurationCurrent * (1 + ListVal("DashingDurationIncrease")) && typ != this && (!Marked.Contains(typ) || MarkedUnitsCanAlsoReset))
                {
                    _dashingCooldown = 0f;

                    if(!Marked.Contains(typ))
                    {
                        Marked.Add(typ);
                        typ.Effects.Add(new Effect(typ, Effect.EffectTypes.Stun, 1, 0.75f*(1-typ.tenacity)));
                        UpdateCombo(Marked.Count);
                        /*Scaling nedenfor er kun til testing*/
                        typ.Scale *= 0.9f;
                    }
                }
            });

        }
        if (_dashingCooldown > 0)
        {
            _dashingCooldown -= Time.deltaTime;
        }

        /*ACTIVATE MARKS*/
        if (References.instance.PlayerInput.Buttons["Attack2"].Pressing &&
            !References.instance.PlayerInput.Buttons["Attack2"].Pressed && Marked.Count > 0)
        {
            References.instance.AspectHandler.UpdateTrigger(AspectTrigger.AspectTriggerType.Finisher, Marked.Count);
            Marked.ForEach(typ =>
            {
                typ.Effects.Add(new Effect(typ, Effect.EffectTypes.DamageDelay, _markDamage,
                    Vector2.Distance(Pos, typ.Pos)/_markedProjectileSpeed));
                _markedProjectiles.Add(new MarkedProjectile(Pos,(typ.Pos - Pos).normalized*_markedProjectileSpeed,GetAngle(typ.Pos), Vector2.Distance(Pos, typ.Pos) / _markedProjectileSpeed));
            }); 
            Marked = new List<IUnit>();
            ResetCombo();
        }

        /*FACING*/
        if (!_dashing)
        {
            _mousefacing = (References.instance.PlayerInput.GetMousePosition() - Pos).normalized;
            if (References.instance.PlayerInput.Dir.magnitude >= 1)
            {
                Rot = GetAngle(Pos + References.instance.PlayerInput.Dir) - 90;
                _facedir = References.instance.PlayerInput.Dir;
            }
        }
        else
        {
            Rot = GetAngle(Pos + _mousefacing) - 90;
        }

        /*GENERIC MOVEMENT*/
        if (!_dashing)
        {
            _speed = References.instance.PlayerInput.Dir*MovementSpeedCurrent*
                        (1 + ListVal("MovementSpeedIncrease"));
        }
        Pos += _speed * Time.deltaTime;

        /*STAND/MOVE TIME*/
        if (_speed == Vector2.zero)
        {
            if (_standingtime == 0) { _standmovetimetriggerdelay = 0f; }
            _standingtime += Time.deltaTime;
            _movetime = 0f;
        }
        else
        {
            if (_movetime == 0) { _standmovetimetriggerdelay = 0f; }
            _movetime += Time.deltaTime;
            _standingtime = 0f;
        }
        if (_standmovetimetriggerdelay <= 0)
        {
            References.instance.AspectHandler.UpdateTrigger(AspectTrigger.AspectTriggerType.MoveTime, _movetime);
            References.instance.AspectHandler.UpdateTrigger(AspectTrigger.AspectTriggerType.StandTime, _standingtime);
            _standmovetimetriggerdelay = _standmovetimetriggerdelaybase;
        }
        else
        {
            _standmovetimetriggerdelay -= Time.deltaTime;
        }

    }
    

    public void Die()
    {
        _markedProjectiles.ForEach(typ => References.Destroy(typ.Body));
        /*
            Spilleren taber!
        */
        base.Die();
    }

    public PlayerController()
    {
        HealthMax = 100;
        HealthCurrent = 100;
        MovementSpeedBase = 3f;

        _markedProjectiles = new List<MarkedProjectile>();

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Player"]);
    }

    float GetAngle(Vector3 targetpos)
    {
        return Mathf.Atan2(targetpos.y - Pos.y, targetpos.x - Pos.x) * 180 / Mathf.PI;
    }

    private class MarkedProjectile
    {
        private readonly Vector2 _speed;
        public readonly GameObject Body;
        public float Lifetime;
        public MarkedProjectile(Vector2 origin,Vector2 speed,float angle,float lifetime)
        {
            _speed = speed;
            Body = References.instance.CreateGameObject(References.instance.PrefabLibrary.Prefabs["MarkedProjectile"]);
            Body.transform.position = origin;
            Body.transform.rotation = Quaternion.Euler(0, 0, angle);
            Lifetime = lifetime;
        }
        public void Update()
        {
            Body.transform.position += (Vector3)(_speed*Time.deltaTime);
            Lifetime -= Time.deltaTime;
        }
    }

}
