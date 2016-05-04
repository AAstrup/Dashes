using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyFactory : IFactory
{
    public void Spawn(RoomLayout layout,List<int> groupNrs)
    {
        if (layout.enemiesSpawned)
            return;

        for (int i = 0; i < layout._enemies.Count; i++)
        {
            if(groupNrs.Contains(layout._enemies[i].groupNr()))
                Spawn(layout._enemies[i]);
        }
    }

    private void Spawn(EnemySpawnInfo info)
    {
        References.instance.SpawnHandler.SpawnEnemy(info.Type(), info);
    }
}
