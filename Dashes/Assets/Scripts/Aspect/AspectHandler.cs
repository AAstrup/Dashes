using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class AspectHandler {

    public List<Aspect> Aspects = new List<Aspect>();
    
    public void AddAspect(Aspect aspect)
    {
        Aspects.Add(aspect);
        Aspects[Aspects.Count - 1].Player = References.instance.UnitHandler.playerController;
        Aspects[Aspects.Count-1].Init();
    }

    public void Init()
    {
        /*Til testing*/
        AddAspect(new Aspect_Cheetah());
    }

    public void Update()
    {
        Aspects.Where(typ => typ.Active).ToList().ForEach(typ => typ.ActiveUpdate());
    }

    public void UpdateTrigger(AspectTrigger.AspectTriggerType type, float value)
    {
        Aspects.Where(typ => typ.Trigger.Type == type).ToList().ForEach(typ =>
        {
            if (value >= typ.Trigger.Value && !typ.Active)
            {
                typ.Activate(value);
            }
            else if(value < typ.Trigger.Value && typ.Active)
            {
                typ.DeActivate();
            }
        });
    }
	
}
