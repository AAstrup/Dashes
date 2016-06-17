using UnityEngine;
using System.Collections.Generic;

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

    public void Init()
    {
        //Possible room loadouts are loaded here. Atm we just add some hardcoded by default
        startLoadout.Add(GetDefaultRoom());
        goalLoadout.Add(GetGoalRoom01());
        rewardLoadout.Add(GetRewardRoom01());
        enemyLoadout.Add(GetDefaultEnemyRoom01());//GetDefaultEnemyRoom01());
    }

    //Should read from file and spawn upon that.

    public RoomLayout GetStartLoadOut()
    {
        return startLoadout[Random.Range(0, startLoadout.Count)];
    }

    public RoomLayout GetGoalLoadOut()
    {
        return goalLoadout[Random.Range(0, goalLoadout.Count)];
    }

    public RoomLayout GetRewardLoadOut()
    {
        return rewardLoadout[Random.Range(0, rewardLoadout.Count)];
    }

    public RoomLayout GetEnemyLoadout()
    {
        return enemyLoadout[Random.Range(0, enemyLoadout.Count)];
    }

    public RoomLayout LoadLoadout()
    {
        return null;
    }

    public RoomLayout GetDefaultRoom()
    {
        return new RoomLayout(new List<EnemySpawnInfo>(), new List<SpawnInfo>() { });
    }

    public RoomLayout GetDefaultEnemyRoom01()
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

        list.Add(new EnemySpawnInfo(2, 3, UnitSpawnType.stupid, GroupType.groupObstacle));
        list.Add(new EnemySpawnInfo(2, -3, UnitSpawnType.stupid, GroupType.groupObstacle));
        list.Add(new EnemySpawnInfo(2, 0, UnitSpawnType.stupid, GroupType.groupObstacle));

        return new RoomLayout(list, new List<SpawnInfo>() { });
    }

    public RoomLayout GetGoalRoom01()
    {
        var goal = new SpawnInfo(0, 0, SpawnInfoType.goal, GroupType.groupStatic);
        return new RoomLayout(new List<EnemySpawnInfo>(), new List<SpawnInfo>() { goal });
    }

    public RoomLayout GetRewardRoom01()
    {
        var pot = new SpawnInfo(1, 1, SpawnInfoType.potion, GroupType.groupStatic);
        return new RoomLayout(new List<EnemySpawnInfo>(), new List<SpawnInfo>() { pot });
    }
}