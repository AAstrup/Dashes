using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyFactory : IFactory
{
    public void Spawn(RoomLayout layout,List<int> groupNrs)
    {
        if (layout.hasSpawned)
            return;

        for (int i = 0; i < layout.GetEnemies().Count; i++)
        {
            if(groupNrs.Contains(layout.GetEnemies()[i].groupNr()))
                Spawn(layout.GetEnemies()[i]);
        }

        for (int i = 0; i < layout.GetPickups().Count; i++)
        {
            if (groupNrs.Contains(layout.GetPickups()[i].groupNr()))
                Spawn(layout.GetPickups()[i]);
        }
    }

    private void Spawn(EnemySpawnInfo info)
    {
        References.instance.SpawnHandler.SpawnEnemy(info.Type(), info);
    }

    private void Spawn(PickupSpawnInfo info)
    {
        References.instance.SpawnHandler.SpawnPickup(info.Type(), info);
    }
}
