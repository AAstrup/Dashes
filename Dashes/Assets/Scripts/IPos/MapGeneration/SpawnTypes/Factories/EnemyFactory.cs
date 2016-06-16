using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyFactory : IFactory
{
    public void Spawn(RoomLayout layout,List<GroupType> groupNrs,RoomScript room)
    {
        if (layout.GetHasSpawned())
            return;

        for (int i = 0; i < layout.GetEnemies().Count; i++)
        {
            if(groupNrs.Contains(layout.GetEnemies()[i].GetGroupType()))
                Spawn(layout.GetEnemies()[i], room);
        }
    }

    private void Spawn(EnemySpawnInfo spawnInfo, RoomScript room)
    {
        References.instance.SpawnHandler.SpawnEnemy(spawnInfo.Type(), spawnInfo, room);
    }
}
