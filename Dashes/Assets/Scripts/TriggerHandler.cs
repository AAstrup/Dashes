using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerHandler {


    public List<ITrigger> triggers;

    public void AddTrigger(ITrigger gmj)
    {
        triggers.Add(gmj);
    }
    public void RemoveTrigger(ITrigger gmj)
    {
        triggers.Remove(gmj);
    }

    public void Init()
    {
        triggers = new List<ITrigger>();

    }

    public void Update()
    {
        triggers.ForEach(typ => typ.Update());
    }

    public void Reset()
    {
        for (int g = 0; g < triggers.Count; g++)
        {
            References.instance.DestroyGameObject(triggers[g].GBref);
        }
        triggers = new List<ITrigger>();
    }
}
