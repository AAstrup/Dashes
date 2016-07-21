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

    public float AttackDamage = 0f;
    public float AttackDamageBase = 4f;

    public float ComboMultiplication = 1f;
    private int _comboSize = 0;
    private float _comboTimeCurrent = 0f;
    private float _comboTimeBase = 1.5f;

    public int ComboMaxSize = 4;

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
        {"MovementSpeedIncrease",new List<float>()},
        {"HealingIncrease",new List<float>()},
        {"DamageIncrease",new List<float>()},
        {"MarkingDamage",new List<float>()}
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

        if (!GetStunned())
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

        /*DASHING*/
        if (_dashing)
        {
            _speed = _mousefacing * _dashingSpeedCurrent;
            _dashingTime -= Time.deltaTime;
            References.instance.particleHandler.Emit(ParticleEffectHandler.particleType.effect_dash, 5, GBref.transform.position,Vector3.Angle(_mousefacing,Vector3.right));
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

            AttackDamage = AttackDamageBase * (1 + ListVal("DamageIncrease"));

            /*Nedenstående skal flyttes over i collision klassen, eller måske ikke... :/ */
            References.instance.UnitHandler.Units.ForEach(typ =>
            {
                var t = Vector3.Cross(_mousefacing, typ.Pos - Pos).magnitude;

                if (t < typ.startScale.x / 2 + startScale.x / 2 - 0.5f && Vector2.Distance(typ.Pos, Pos) < _dashingSpeedCurrent * DashingDurationCurrent * (1 + ListVal("DashingDurationIncrease")) && typ != this && (!Marked.Contains(typ) || MarkedUnitsCanAlsoReset))
                {
                    _dashingCooldown = 0f;

                    if(!Marked.Contains(typ))
                    {
                        Marked.Add(typ);
                        typ.VisualMarked();
						typ.Damage(AttackDamage * ListVal("MarkingDamage"));
                        typ.Effects.Add(new Effect(typ, Effect.EffectTypes.Stun, 1, 1.25f*(1-typ.tenacity)));
                        UpdateCombo(Marked.Count);

                        _comboSize = Mathf.Min(_comboSize+1,ComboMaxSize);
                        _comboTimeCurrent = _comboTimeBase;

                        if (_comboSize >= 2)
                        {
                            References.instance.UIHandler.EnableCombo();
                            References.instance.UIHandler.UpdateComboFill((float)(_comboSize-1) / (ComboMaxSize-1));
                        }
                    }
                }
            });

        }
        if (_dashingCooldown > 0)
        {
            _dashingCooldown -= Time.deltaTime;
            References.instance.UIHandler.UpdateBar("AgBar", 1-_dashingCooldown / DashingCooldownDuration,false);
        }

        /*COMBO*/
        if (_comboSize > 0)
        {
            _comboTimeCurrent -= Time.deltaTime;
            References.instance.UIHandler.UpdateCombo(_comboSize,_comboTimeCurrent/_comboTimeBase,(float)_comboSize/ComboMaxSize);
            if (_comboTimeCurrent <= 0)
            {
                _comboSize = 0;
                References.instance.UIHandler.DisableCombo();
            }
        }

        /*ACTIVATE MARKS, Release combo*/
        if (References.instance.PlayerInput.Buttons["Attack2"].Pressing &&
            !References.instance.PlayerInput.Buttons["Attack2"].Pressed && Marked.Count > 0)
        {
            References.instance.cameraScript.ScreenShake(  Mathf.Pow(_comboSize,0.4f)*0.125f+0.05f  ); //0.25f var tidligere default
            References.instance.AspectHandler.UpdateTrigger(AspectTrigger.AspectTriggerType.Finisher, Marked.Count);
            Marked.ForEach(typ =>
            {
                typ.VisualUnMarked();
                //typ.Effects.Add(new Effect(typ, Effect.EffectTypes.Stun, 1, 1.25f * (1 - typ.tenacity)));
                typ.Effects.Add(new Effect(typ, Effect.EffectTypes.DamageDelay, AttackDamage*(1+_comboSize*ComboMultiplication*BigEnough(_comboSize,2)),
                    Vector2.Distance(Pos, typ.Pos)/_markedProjectileSpeed));
                _markedProjectiles.Add(new MarkedProjectile(Pos,(typ.Pos - Pos).normalized*_markedProjectileSpeed,GetAngle(typ.Pos), Vector2.Distance(Pos, typ.Pos) / _markedProjectileSpeed));
            });
            _comboSize = 0;
            References.instance.UIHandler.DisableCombo();
            Marked = new List<IUnit>();
            ResetCombo();
        }

        /*FACING*/
        if (!_dashing)
        {
            _mousefacing = (References.instance.PlayerInput.GetMousePosition() - Pos).normalized;
            if (References.instance.PlayerInput.Dir.magnitude >= 1)
            {
                Rot = GetAngle(Pos + References.instance.PlayerInput.Dir);
                _facedir = References.instance.PlayerInput.Dir;
            }
        }
        else
        {
            Rot = GetAngle(Pos + _mousefacing);
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

    float BigEnough(float value, float requirement)
    {
        return value >= requirement ? 1f : 0f;
    }


    public override void Die()
    {
        //_markedProjectiles.ForEach(typ => References.Destroy(typ.Body));
        /*
            Spilleren taber!
        */
        //base.Die();
        References.instance.Reload();
    }

    public PlayerController()
    {
        HealthMax = 10;
        HealthCurrent = 10;
        MovementSpeedBase = 3f;

        _markedProjectiles = new List<MarkedProjectile>();

        GenericConstructor(References.instance.PrefabLibrary.Prefabs["Player"]);

        //References.instance.UIHandler.ArrangeHearts((int)HealthMax); Kaldes i UIHandleren i stedet
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

    public override void Damage(float amount)
    {
        References.instance.AspectHandler.UpdateTrigger(AspectTrigger.AspectTriggerType.Damage, amount);
        base.Damage(amount);

        References.instance.UIHandler.UpdateBloodDamage(HealthCurrent/HealthMax);

        References.instance.UIHandler.UpdateHearts(HealthCurrent);
        //References.instance.UIHandler.UpdateBar("HealthBar",HealthCurrent/HealthMax,true);
    }

    public override void Heal(float amount)
    {
        References.instance.AspectHandler.UpdateTrigger(AspectTrigger.AspectTriggerType.Heal, amount);
        base.Heal(amount * (1 + ListVal("HealingIncrease")));

        References.instance.UIHandler.UpdateBloodHeal(HealthCurrent / HealthMax);

        References.instance.UIHandler.UpdateHearts(HealthCurrent);
        //References.instance.UIHandler.UpdateBar("HealthBar", HealthCurrent / HealthMax,true);
    }
}
