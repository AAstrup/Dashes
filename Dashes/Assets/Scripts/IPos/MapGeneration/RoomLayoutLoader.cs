using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Creates a list of enemyspaws, and regularspawns.
/// </summary>
public class RoomLayoutLoader
{
    string path = "";
    List<RoomLayout> startLoadout = new List<RoomLayout>();
    List<RoomLayout> goalLoadout = new List<RoomLayout>();
    List<RoomLayout> rewardLoadout = new List<RoomLayout>();
    List<RoomLayout> enemyLoadout = new List<RoomLayout>();
    List<RoomLayout> bossLoadout = new List<RoomLayout>();

    public void Init()
    {
        //Possible room loadouts are loaded here. Atm we just add some hardcoded by default
        startLoadout.Add(GetDefaultRoom());
        goalLoadout.Add(GetGoalRoom01());
        rewardLoadout.Add(GetRewardRoom01());
        enemyLoadout.Add(GetEnemyRoom01());//GetDefaultEnemyRoom01());
        bossLoadout.Add(GetBossRoom01());
    }

    //Should read from file and spawn upon that.

    public RoomLayout GetStartLoadOut()
    {
        return startLoadout[UnityEngine.Random.Range(0, startLoadout.Count)];
    }

    public RoomLayout GetGoalLoadOut()
    {
        return goalLoadout[UnityEngine.Random.Range(0, goalLoadout.Count)];
    }

    public RoomLayout GetBossLoadOut()
    {
        return bossLoadout[UnityEngine.Random.Range(0, goalLoadout.Count)];
    }

    public RoomLayout GetRewardLoadOut()
    {
        return rewardLoadout[UnityEngine.Random.Range(0, rewardLoadout.Count)];
    }

    public RoomLayout GetEnemyLoadout()
    {
        return enemyLoadout[UnityEngine.Random.Range(0, enemyLoadout.Count)];
    }

    public RoomLayout LoadLoadout()
    {
        return null;
    }

    public RoomLayout GetDefaultRoom()
    {
        return new RoomLayout(new List<EnemySpawnInfo>(), new List<SpawnInfo>() { });
    }

    public RoomLayout GetEnemyRoom01()
    {
        var list = new List<EnemySpawnInfo>();
        
        list.Add(new EnemySpawnInfo(2, 1, UnitSpawnType.stupid, GroupType.groupHorde));
        list.Add(new EnemySpawnInfo(2, -1, UnitSpawnType.stupid, GroupType.groupHorde));
        list.Add(new EnemySpawnInfo(3, 1, UnitSpawnType.stupid, GroupType.groupHorde));
        list.Add(new EnemySpawnInfo(3, -1, UnitSpawnType.stupid, GroupType.groupHorde));

        list.Add(new EnemySpawnInfo(1, 1, UnitSpawnType.stupid, GroupType.groupAntiCamp));
        list.Add(new EnemySpawnInfo(1, -1, UnitSpawnType.antiCamp, GroupType.groupAntiCamp));

        list.Add(new EnemySpawnInfo(-2, -1, UnitSpawnType.threat, GroupType.groupThreat));
        list.Add(new EnemySpawnInfo(-2, 1, UnitSpawnType.threat, GroupType.groupThreat));

        list.Add(new EnemySpawnInfo(2, 3, UnitSpawnType.obstacle, GroupType.groupObstacle));
        list.Add(new EnemySpawnInfo(2, -3, UnitSpawnType.obstacle, GroupType.groupObstacle));
        list.Add(new EnemySpawnInfo(2, 0, UnitSpawnType.obstacle, GroupType.groupObstacle));

        return new RoomLayout(list, new List<SpawnInfo>() { });
    }

    public RoomLayout GetGoalRoom01()
    {
        var goal = new SpawnInfo(0, 0, SpawnInfoType.goal, GroupType.groupStatic);
        return new RoomLayout(new List<EnemySpawnInfo>() { }, new List<SpawnInfo>() { goal });
    }


    public RoomLayout GetRewardRoom01()
    {
        var pot = new SpawnInfo(1, 1, SpawnInfoType.potion, GroupType.groupStatic);
        return new RoomLayout(new List<EnemySpawnInfo>(), new List<SpawnInfo>() { pot });
    }

    private RoomLayout GetBossRoom01()
    {
        var boss = new EnemySpawnInfo(0, 0, UnitSpawnType.boss, GroupType.groupStatic);
        return new RoomLayout(new List<EnemySpawnInfo>() { boss }, new List<SpawnInfo>() { });
    }
}