using UnityEngine;
using System.Collections.Generic;

public class RoomLayoutLoader
{
    string path = "";
    List<RoomLayout> startLoadout = new List<RoomLayout>();
    List<RoomLayout> goalLoadout = new List<RoomLayout>();
    List<RoomLayout> rewardLoadout = new List<RoomLayout>();
    List<RoomLayout> enemyLoadout = new List<RoomLayout>();

    public void Init()
    {
        startLoadout.Add(GetDefaultRoom());
        goalLoadout.Add(GetDefaultRoom());
        rewardLoadout.Add(GetDefaultRoom());
        enemyLoadout.Add(GetDefaultEnemyRoom());
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
        return new RoomLayout(new List<EnemySpawnInfo>(), new List<PickupSpawnInfo>() { });
    }

    public RoomLayout GetDefaultEnemyRoom()
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

        return new RoomLayout(list, new List<PickupSpawnInfo>() { });
    }
}