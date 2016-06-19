using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerHandler {


    List<ITrigger> triggers;

    public void AddTrigger(ITrigger trigger)
    {
        triggers.Add(trigger);
    }
    public void RemoveTrigger(ITrigger trigger)
    {
        triggers.Remove(trigger);
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
