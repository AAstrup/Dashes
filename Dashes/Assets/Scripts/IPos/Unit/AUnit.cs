using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public abstract class IUnit : Position {

    public float HealthMax;
    public float HealthCurrent;
    public float MovementSpeedBase;
    public float MovementSpeedCurrent;
    protected bool marked = false;

    public float tenacity = 0f;//goes from 0 - 1
    private bool Stunned;
    public float Slow;
    public float Boost;
    public List<Effect> Effects; 

    public Vector2 startScale;
    public float Rot;
    //Juice hitEffect
    float juiceHitEffectAmount = 1.33f;
    float juiceHitEffectTimeMultiplier = 2f;

    public bool Damaged = false;

    public GameObject GBref;
    public SpriteRenderer GBSpriteRenderer;

    public bool Invulnerable = false;

    public virtual void Update()
    {
        Effects.ForEach(typ => typ.Update(this));
        var col = References.instance.colSystem.CollidesWithWall(this);
        CollisionEvent(col);
        GBref.transform.position = Pos;
        GBref.transform.rotation = Quaternion.Euler(0, 0, Rot);

        if(GBref.transform.localScale.x > startScale.x)
        {
            var scale = GBref.transform.localScale.x - (juiceHitEffectTimeMultiplier * Time.deltaTime);
            if (scale < startScale.x)
                scale = startScale.x;
            GBref.transform.localScale = new Vector3(scale, scale, 1f);            
        }
    }

    protected virtual void CollisionEvent(Collision col)
    {
        Pos = col.GetFinalPos();
    }

    public virtual void Damage(float amount)
    {
        if (!Invulnerable)
        {
            HealthCurrent -= amount;
            CreateBloodDetail(2.0f);

            if (HealthCurrent <= 0)
            {
                CreateBloodDetail(4f);
                Die();
            }
            else
                JuiceEffect_Size();
        }
    }

    public void CreateBloodDetail(float multiplier)
    {
        string bloodIndex = Mathf.FloorToInt(Random.Range(1, 5)).ToString();
        References.instance.particleHandler.Emit(ParticleEffectHandler.particleType.effect_bleed, Mathf.CeilToInt(multiplier * 5),Pos);
        var gmj = References.instance.CreateGameObject(References.instance.PrefabLibrary.Prefabs["Detail_Blood"+ bloodIndex]);
        gmj.transform.position = GBref.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f),0);
        gmj.transform.localScale = new Vector3(Random.Range(0.2f, 0.3f) * multiplier, Random.Range(0.2f, 0.3f) * multiplier, 1f);
        gmj.transform.rotation = Quaternion.Euler(0,0, Random.Range(0, 360));
        References.instance.DetailHandler.AddDetail(gmj);
    }

    private void JuiceEffect_Size()
    {
        GBref.transform.localScale = new Vector3(startScale.x * juiceHitEffectAmount, startScale.y * juiceHitEffectAmount, 1f);
    }

    public virtual void Heal(float amount)
    {
        HealthCurrent = Mathf.Min(HealthCurrent+amount,HealthMax);
    }

    public virtual void Die()
    {
        References.instance.UnitHandler.Units.Remove(this);
        References.instance.DestroyGameObject(GBref);
    }

    public void GenericConstructor(GameObject prefab)
    {
        MovementSpeedCurrent = MovementSpeedBase;
        Effects = new List<Effect>();
        Stunned = false;
        Slow = 0f;
        GBref = References.instance.CreateGameObject(prefab);
        GBSpriteRenderer = GBref.GetComponent<SpriteRenderer>();
        startScale = Vector2.one;
    }

    public void UpdateMovementSpeed()
    {
        MovementSpeedCurrent = MovementSpeedBase*(1 - Slow)*(1 + Boost);
    }

    public void VisualMarked()
    {
        marked = true;
        GBSpriteRenderer.color = new Color(GBSpriteRenderer.color.r/2f, GBSpriteRenderer.color.g/2f, GBSpriteRenderer.color.b/2f);
    }
    public void VisualUnMarked()
    {
        marked = false;
        GBSpriteRenderer.color = new Color(GBSpriteRenderer.color.r * 2f, GBSpriteRenderer.color.g * 2f, GBSpriteRenderer.color.b * 2f);
    }

    protected void SetVisualColors(Color c,string caller)
    {
        if (marked)
            GBSpriteRenderer.color = new Color(c.r / 2f, c.g / 2f, c.b / 2f);
        else
            GBSpriteRenderer.color = c;
    }

    public virtual void SetStunned(bool v) { Stunned = v; }

    public virtual bool GetStunned() { return Stunned; }
}
