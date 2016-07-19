using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Editor_InformationHandler  {

    public Editor_RoomLayout layout;
    public Editor_IHasPosition[,] entities;

    public void Init()
    {
        layout = new Editor_RoomLayout();
        layout.Init();

        entities = new Editor_IHasPosition[12, 7];//if units had a radius of 0, it would be an array[16,10], additionally we have the walls which reduces it addtionally from [15,9] to [12,7]
    }

    public void AddEnemy(EnemySpawnInfo enemy)
    {
        layout.enemieInfos.Add(enemy);
        AddToInterface(enemy);
        AddEnemyValue(enemy);
    }

    public void RemoveEnemy(Vector2 worldPos)
    {
        EnemySpawnInfo toRemove = null;
        for (int e = 0; e < layout.enemieInfos.Count; e++)
        {
            if (layout.enemieInfos[e].GetX() == worldPos.x && layout.enemieInfos[e].GetY() == worldPos.y)
            {
                toRemove = layout.enemieInfos[e];
                break;
            }
        }
        if (toRemove == null)
        {
            Debug.Log("No enemy found at that position, might be due to float? Pos: " + worldPos.ToString());
            return;
        }
        RemoveEnemyValue(toRemove);
        layout.enemieInfos.Remove(toRemove);
        RemoveFromInterface(toRemove);
        Editor_References.instance.DestroyGMJ(toRemove.GBRef);
    }
    public void AddEnemyValue(EnemySpawnInfo enemy)
    {
        Editor_References.instance.UIHandler.groupTypeToBlock[enemy._groupType].Increase(Editor_References.instance.UIHandler.unitTypeCost[enemy._type]);
    }
    public void RemoveEnemyValue(EnemySpawnInfo enemy)
    {
        Editor_References.instance.UIHandler.groupTypeToBlock[enemy._groupType].Increase(- Editor_References.instance.UIHandler.unitTypeCost[enemy._type]);
    }


    public void Load(Editor_RoomLayout loadedObj)
    {
        layout = loadedObj;
        Editor_References.instance.drawer.SetUpDoorVisuals();
        foreach (var enemy in layout.enemieInfos)
        {
            enemy.LoadSetup();
            AddToInterface(enemy);
            AddEnemyValue(enemy);
        }
        foreach (var item in layout.pickupInfos)
        {
            item.LoadSetup();
            AddToInterface(item);
            AddItemValue(item);
        }
    }

    public void Reset()
    {
        foreach (var entity in entities)
        {
            if(entity != null)
                Editor_References.instance.DestroyGMJ(entity.GetGMJ());
        }
        Editor_References.instance.UIHandler.Reset();
        Init();
    }

    private void AddToInterface(Editor_IHasPosition entity)
    {
        var pos = entity.GetPosition(); ;
        entities[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)] = entity;
    }
    private void RemoveFromInterface(Editor_IHasPosition entity)
    {
        var pos = entity.GetPosition(); ;
        entities[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)] = null;
    }

    internal void AddPickUp(ItemSpawnInfo itemSpawnInfo)
    {
        layout.pickupInfos.Add(itemSpawnInfo);
        AddToInterface(itemSpawnInfo);
        AddItemValue(itemSpawnInfo);
    }

    internal void RemovePickup(Vector2 worldPos)
    {
        ItemSpawnInfo toRemove = null;
        for (int e = 0; e < layout.pickupInfos.Count; e++)
        {
            if (layout.pickupInfos[e].GetX() == worldPos.x && layout.pickupInfos[e].GetY() == worldPos.y)
            {
                toRemove = layout.pickupInfos[e];
                break;
            }
        }
        if (toRemove == null)
        {
            Debug.Log("No pickup found at that position, might be due to float? Pos: " + worldPos.ToString());
            return;
        }
        RemoveItemValue(toRemove);
        layout.pickupInfos.Remove(toRemove);
        RemoveFromInterface(toRemove);
        Editor_References.instance.DestroyGMJ(toRemove.GBRef);
    }
    public void AddItemValue(ItemSpawnInfo itemSpawnInfo)
    {
        Editor_References.instance.UIHandler.groupTypeToBlock[itemSpawnInfo._groupType].Increase(Editor_References.instance.UIHandler.itemTypeCost[itemSpawnInfo._type]);
    }
    public void RemoveItemValue(ItemSpawnInfo itemSpawnInfo)
    {
        Editor_References.instance.UIHandler.groupTypeToBlock[itemSpawnInfo._groupType].Increase(- Editor_References.instance.UIHandler.itemTypeCost[itemSpawnInfo._type]);
    }

}
