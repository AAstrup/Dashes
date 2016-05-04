using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerHandler {


    public List<ITrigger> triggers;

    public void Init()
    {
        triggers = new List<ITrigger>();

    }

    public void Update()
    {

        triggers.ForEach(typ => typ.Update());

    }
}
