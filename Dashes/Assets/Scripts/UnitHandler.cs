using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UnitHandler
{

    public List<IUnit> Units;
    public IUnit playerIUnit;
    public PlayerController playerController;

    public void Init()
    {
        Units = new List<IUnit>();

        /*Nedestående er kun til testing*/
        playerIUnit = CreatePlayer();
        Units.Add(playerIUnit);
    }

    public IUnit CreatePlayer()
    {
        var temp = new PlayerController();
        playerController = temp;
        return temp;
    }

    public void Update()
    {   
        Units.ForEach(typ => typ.Update());
    }


}
