using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyFactory : IFactory
{
    public void Spawn(RoomLayout layout,List<GroupType> groupNrs)
    {
        if (layout.GetHasSpawned())
            return;

        for (int i = 0; i < layout.GetEnemies().Count; i++)
        {
            if(groupNrs.Contains(layout.GetEnemies()[i].GetGroupType()))
                Spawn(layout.GetEnemies()[i]);
        }
    }

    private void Spawn(EnemySpawnInfo info)
    {
        References.instance.SpawnHandler.SpawnEnemy(info.Type(), info);
    }
}
