using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnHandler {
    Dictionary<EnemyType, List<IUnitType>> enemytypeLevelSpawn;
    public void Init(List<IUnitType> stupids,List<IUnitType> antiCamp,List<IUnitType> threats,List<IUnitType> obstacles)
    {
        enemytypeLevelSpawn = new Dictionary<EnemyType, List<IUnitType>>();
        enemytypeLevelSpawn.Add(EnemyType.stupid, stupids);
        enemytypeLevelSpawn.Add(EnemyType.antiCamp, antiCamp);
        enemytypeLevelSpawn.Add(EnemyType.threat, threats);
        enemytypeLevelSpawn.Add(EnemyType.obstacle, obstacles);
    }

    public void SpawnEnemy(EnemyType spawnType,SpawnInfo spawn)
    {
        var type = spawnType;
        if (enemytypeLevelSpawn[type].Count == 0)
            type = EnemyType.stupid;

        CreateEnemy(enemytypeLevelSpawn[type][Mathf.FloorToInt(Random.Range(0, enemytypeLevelSpawn[type].Count))],new Vector2(spawn.x(), spawn.y()));
    }

    private void CreateEnemy(IUnitType enemyType, Vector2 pos)
    {
        IUnit enemy;
        if (enemyType == IUnitType.Enemy_Stupid)
            enemy = new Stupid(References.instance.UnitHandler.playerController);
        else if (enemyType == IUnitType.Enemy_Charger)
            enemy = new Charger(References.instance.UnitHandler.playerController);
        else //if (enemyType == IUnitType.Enemy_Archer)
            enemy = new Archer(References.instance.UnitHandler.playerController);

        enemy.Pos = pos + References.instance.RoomHandler.GetCurrentRoom().GetWorldPos();
        References.instance.RoomHandler.UnitSpawned(enemy);
    }

}
public enum EnemyType { stupid, antiCamp, threat, obstacle }
public enum IUnitType { Enemy_Stupid, Enemy_Archer, Enemy_Charger}
