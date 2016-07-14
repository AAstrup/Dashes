using UnityEngine;
using System.Collections;
using System;

public class Editor_Input {

    public void Update()
    {
        if (Editor_References.instance.UIHandler.inputField.isFocused)
            return;

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Saved");
            Editor_References.instance.saver.Save(Editor_References.instance.UIHandler.inputField.text);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Load");
            Editor_References.instance.loader.Editor_LoadXML(Editor_References.instance.UIHandler.inputField.text);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Editor_References.instance.drawer.IncreaseUnitType();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Editor_References.instance.drawer.IncreaseItemType();
        }//
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Editor_References.instance.drawer.IncreaseRoomType();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Editor_References.instance.drawer.IncreaseGroupType();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Editor_References.instance.drawer.IncreaseOrientation();
        }
    }

    public void Init()
    {

    }
}
