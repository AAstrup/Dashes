using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Editor_UIEvents : MonoBehaviour {

    public InputField inputField;
    public void SelectGroupType(string groupType) {
        Editor_References.instance.drawer.groupType = (GroupType) Enum.Parse(typeof (GroupType), groupType);
    }
    public void SelectEnemyType(string enemyType) {
        Editor_References.instance.drawer.unitType = (UnitSpawnType)Enum.Parse(typeof(UnitSpawnType), enemyType);
    }
    public void SetOrientation()
    {
        throw new NotImplementedException();
    }

    public void SetRoomType()
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Editor_References.instance.drawer.Draw(GetMouseWorldPosition());
        else if (Input.GetMouseButtonDown(1))
            Editor_References.instance.drawer.Delete(GetMouseWorldPosition());
    }

    Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }
}
