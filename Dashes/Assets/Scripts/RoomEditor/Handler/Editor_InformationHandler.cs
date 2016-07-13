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
        layout.enemieInfos.Remove(toRemove);
        RemoveFromInterface(toRemove);
        Editor_References.instance.DestroyGMJ(toRemove.GBRef);
    }

    public void Load(Editor_RoomLayout loadedObj)
    {
        layout = loadedObj;
        foreach (var enemy in layout.enemieInfos)
        {
            enemy.LoadSetup();
        }
        foreach (var item in layout.pickupInfos)
        {
            item.LoadSetup();
        }
    }

    public void Reset()
    {
        foreach (var entity in entities)
        {
            if(entity != null)
                Editor_References.instance.DestroyGMJ(entity.GetGMJ());
        }
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
}
