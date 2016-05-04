using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoomLayoutHandler {

    public void Init()
    {

    }

    public RoomLayout LoadLoadout(RoomScript roomScript)
    {
        if (roomScript.GetRoomType() == RoomScript.roomType.E)
            return LoadEnemyLayout();
        if (roomScript.GetRoomType() == RoomScript.roomType.R)
            return LoadRewardLayout();
        if (roomScript.GetRoomType() == RoomScript.roomType.G)
            return LoadGoalLayout();
        else if (roomScript.GetRoomType() == RoomScript.roomType.S)
            return LoadStartLayout();
        else
            throw new Exception("BAD ROOM TYPE");
    }

    private RoomLayout LoadStartLayout()
    {
        List<EnemySpawnInfo> enemySpawnList = new List<EnemySpawnInfo>() { };
        return new RoomLayout(enemySpawnList);
    }

    private RoomLayout LoadGoalLayout()
    {
        List<EnemySpawnInfo> enemySpawnList = new List<EnemySpawnInfo>() { };
        return new RoomLayout(enemySpawnList);
    }

    private RoomLayout LoadRewardLayout()
    {
        List<EnemySpawnInfo> enemySpawnList = new List<EnemySpawnInfo>() {};
        return new RoomLayout(enemySpawnList);
    }

    public RoomLayout LoadEnemyLayout()
    {
        var spawnI1 = new EnemySpawnInfo(3, 0, EnemyType.stupid, 0);
        var spawnI2 = new EnemySpawnInfo(0, 3, EnemyType.stupid, 1);
        List<EnemySpawnInfo> enemySpawnList = new List<EnemySpawnInfo>() { spawnI1, spawnI2 };
        return new RoomLayout(enemySpawnList);
    }
}
