using UnityEngine;
using System.Collections;

public abstract class Aspect
{

    public AspectTrigger Trigger = new AspectTrigger();
    public bool Passive = false;
    public bool Active = false;
    public PlayerController Player;

    public string Title;
    public string Description;

    public abstract void Activate(float triggervalue);
    public abstract void DeActivate();
    public abstract void Init();
    public virtual void ActiveUpdate()
    {
    }

}