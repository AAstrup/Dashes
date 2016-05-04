using UnityEngine;
using System.Collections;

public class AspectTrigger
{

    public AspectTriggerType Type;
    public float Value;

    public enum AspectTriggerType
    {
        Combo,
        MoveTime,
        StandTime,
        Health,
        Damage,
        Finisher,
        None
    }

    public AspectTrigger()
    {
        Type = AspectTriggerType.None;
        Value = 0f;
    }
    public AspectTrigger(AspectTriggerType type, float value)
    {
        Type = type;
        Value = value;
    }

}
