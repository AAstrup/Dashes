using UnityEngine;
using System.Collections;
using System;

public class Editor_Drawer  {

    enum Drawer_Pencil { UnitDrawer, PickupDrawer };
    Drawer_Pencil pencil = Drawer_Pencil.UnitDrawer;

    public UnitSpawnType unitType = UnitSpawnType.stupid;
    public GroupType groupType = GroupType.groupHorde;
    public SpawnInfoType itemType = SpawnInfoType.potion;

    public void IncreaseUnitType()
    {
        pencil = Drawer_Pencil.UnitDrawer;
        int m = Enum.GetNames(typeof(UnitSpawnType)).Length;
        int enumInt = (int) (unitType + 1) % m;
        unitType = (UnitSpawnType) enumInt;
        Debug.Log("UnitType = " + unitType.ToString());
    }

    public void IncreaseGroupType()
    {
        int m = Enum.GetNames(typeof(GroupType)).Length;
        int enumInt = (int)(groupType + 1) % m;
        groupType = (GroupType)enumInt;
        Debug.Log("GroupType = " + groupType.ToString());
    }

    public void IncreaseRoomType()
    {
        int m = Enum.GetNames(typeof(SpawnInfoType)).Length;
        int enumInt = (int)(Editor_References.instance.handler.layout.layoutInfo.roomType + 1) % m;
        Editor_References.instance.handler.layout.layoutInfo.roomType = (RoomScript.roomType)enumInt;
        Debug.Log("RoomType = " + Editor_References.instance.handler.layout.layoutInfo.roomType.ToString());
    }

    public void IncreaseItemType()
    {
        pencil = Drawer_Pencil.PickupDrawer;
        int m = Enum.GetNames(typeof(SpawnInfoType)).Length;
        int enumInt = (int)(itemType + 1) % m;
        itemType = (SpawnInfoType)enumInt;
        Debug.Log("ItemType = " + itemType.ToString());
    }

    bool LegitDrawPosition(Vector2 worldPos)
    {
        if (worldPos.x < 0 || worldPos.y < 0)
            return false;
        if (worldPos.x >= Editor_References.instance.handler.entities.GetLength(0) || worldPos.y >= Editor_References.instance.handler.entities.GetLength(1))
            return false;
        return true;
    }
    bool SpotIsFree(Vector2 worldPos)
    {
        Vector2 debug = new Vector2(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));
        Debug.Log("Testing at " + debug.ToString());
        return Editor_References.instance.handler.entities[Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y)] == null;
    }

    Vector2 VectorToVectorGrid(Vector2 input)
    {
        return new Vector2(Mathf.Round(input.x), Mathf.Round(input.y));
    }

    public void Draw(Vector2 mouseworldPos)
    {
        var worldPos = VectorToVectorGrid(mouseworldPos);
        if (!LegitDrawPosition(worldPos))
            return;
        if (!SpotIsFree(worldPos))
            return;
        if (pencil == Drawer_Pencil.UnitDrawer)
            Editor_References.instance.handler.AddEnemy(new EnemySpawnInfo(worldPos.x, worldPos.y, unitType, groupType));
        else if (pencil == Drawer_Pencil.PickupDrawer)
            Editor_References.instance.handler.AddPickUp(new ItemSpawnInfo(worldPos.x, worldPos.y, itemType, groupType));
    }
    public void Delete(Vector2 mouseworldPos)
    {
        var worldPos = VectorToVectorGrid(mouseworldPos);
        if (SpotIsFree(worldPos))
            return;
        if (pencil == Drawer_Pencil.UnitDrawer)
            Editor_References.instance.handler.RemoveEnemy(worldPos);
        else if (pencil == Drawer_Pencil.PickupDrawer)
            Editor_References.instance.handler.RemovePickup(worldPos);
    }

    public void Init()
    {

    }
}