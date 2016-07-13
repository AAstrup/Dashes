using UnityEngine;
using System.Collections;
using System;

public class Editor_Input {

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Editor_References.instance.drawer.unitType = UnitSpawnType.stupid;
            Debug.Log("UnitSpawnType = stupid");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Editor_References.instance.drawer.unitType = UnitSpawnType.antiCamp;
            Debug.Log("UnitSpawnType = antiCamp");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("UnitSpawnType = threat");
            Editor_References.instance.drawer.unitType = UnitSpawnType.threat;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("UnitSpawnType = obstacle");
            Editor_References.instance.drawer.unitType = UnitSpawnType.obstacle;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("UnitSpawnType = boss");
            Editor_References.instance.drawer.unitType = UnitSpawnType.boss;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Saved");
            Editor_References.instance.saver.Save();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Load");
            Editor_References.instance.loader.Editor_LoadXML();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Editor_References.instance.drawer.IncreaseUnitType();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Editor_References.instance.drawer.IncreaseGroupType();
        }
    }

    public void Init()
    {

    }
}
