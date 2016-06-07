using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public abstract class IUnit : Position {

    public float HealthMax;
    public float HealthCurrent;
    public float MovementSpeedBase;
    public float MovementSpeedCurrent;

    public float tenacity = 0f;//goes from 0 - 1
    public bool Stunned;
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
        Pos = References.instance.colSystem.CollidesWithWall(this).GetFinalPos();
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

    public virtual void Damage(float amount)
    {
        if (!Invulnerable)
        {
            HealthCurrent -= amount;

            if (HealthCurrent <= 0)
            {
                Die();
            }
            else
                JuiceEffect_Size();
        }
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
        GBSpriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
    }
    public void VisualUnMarked()
    {
        GBSpriteRenderer.color = new Color(1f,1f,1f);
    }
}
