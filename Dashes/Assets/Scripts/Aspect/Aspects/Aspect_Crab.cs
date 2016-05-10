using UnityEngine;
using System.Collections;

public class Aspect_Crab : Aspect
{

    public override void Activate(float triggervalue)
    {
        Player.Marked.ForEach(typ =>
        {
            if (!typ.Damaged)
            {
                typ.Damage(References.instance.UnitHandler.playerController.AttackDamage * 0.25f);
            }
        });
        Active = true;
        DeActivate();
    }

    public override void DeActivate()
    {
        Active = false;
    }

    public override void Init()
    {

    }

    public Aspect_Crab()
    {
        Trigger = new AspectTrigger(AspectTrigger.AspectTriggerType.Finisher, 1);
        Passive = false;

        Title = "Crab aspect";
        Description = "Undamaged enemies take 25% increased damage when finishing your combo.";
    }

}