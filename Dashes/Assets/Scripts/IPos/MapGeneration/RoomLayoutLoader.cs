using UnityEngine;
using System.Collections.Generic;
using System;

public class RoomLayoutLoader
{
    string path = "";
    public void Init()
    {
        
    }

    //Should read from file and spawn upon that.

    public RoomLayout GetStartLoadOut()
    {
        //var spawnI1 = new COIN(3, 0, UnitSpawnType.antiCamp, 0);
        return new RoomLayout(new List<EnemySpawnInfo>(), new List<PickupSpawnInfo>() { });
    }

    public RoomLayout GetGoalLoadOut()
    {
        //var spawnI1 = new COIN(3, 0, UnitSpawnType.antiCamp, 0);
        return new RoomLayout(new List<EnemySpawnInfo>(), new List<PickupSpawnInfo>() { });
    }

    public RoomLayout GetRewardLoadOut()
    {
        //var spawnI1 = new COIN(3, 0, UnitSpawnType.antiCamp, 0);
        return new RoomLayout(new List<EnemySpawnInfo>(), new List<PickupSpawnInfo>() { });
    }

    public RoomLayout GetEnemyLoadOut()
    {
        var spawnI1 = new EnemySpawnInfo(3, 0, UnitSpawnType.antiCamp, 0);
        var spawnI2 = new EnemySpawnInfo(0, 3, UnitSpawnType.antiCamp, 1);
        return new RoomLayout(new List<EnemySpawnInfo>() { spawnI1, spawnI2 }, new List<PickupSpawnInfo>());
    }

}