using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UnitHandler
{
    public List<IUnit> Units;
    public List<IUnit> DeadUnitsInRoom;
    public IUnit playerIUnit;
    public PlayerController playerController;

    public bool UnitPause = false;

    public void Init()
    {
        Units = new List<IUnit>();
        DeadUnitsInRoom = new List<IUnit>();

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
        if (UnitPause)
        {
            if (Input.anyKeyDown)
            {
                UnitPause = false;
            }
        }
        else
        {
            Units.ForEach(typ => typ.Update());
        }

        /*if (Time.timeScale == 0f)
        {
            if (Input.anyKeyDown)
            {
                Time.timeScale = 1f;//Unpause game. Is paused when a new room is loaded.
                References.instance.UIHandler.DebugLogClear();
            }
        }*/
    }

    public void Reset()
    {
        Units = new List<IUnit>() { playerIUnit };
        DeadUnitsInRoom = new List<IUnit>();
    }
}
