using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnHandler {
    Dictionary<UnitSpawnType, List<UnitType>> possibleEnemies;
    Dictionary<SpawnInfoType, List<SpawnType>> possibleRegularSpawns;
    public void Init(List<UnitType> stupids,List<UnitType> antiCamp,List<UnitType> threats,List<UnitType> obstacles, List<UnitType> boss)
    {
        possibleEnemies = new Dictionary<UnitSpawnType, List<UnitType>>();
        possibleEnemies.Add(UnitSpawnType.stupid, stupids);
        possibleEnemies.Add(UnitSpawnType.antiCamp, antiCamp);
        possibleEnemies.Add(UnitSpawnType.threat, threats);
        possibleEnemies.Add(UnitSpawnType.obstacle, obstacles);
        possibleEnemies.Add(UnitSpawnType.boss, boss);

        possibleRegularSpawns = new Dictionary<SpawnInfoType, List<SpawnType>>();
        possibleRegularSpawns.Add(SpawnInfoType.goal, new List<SpawnType>() { SpawnType.goal });
        possibleRegularSpawns.Add(SpawnInfoType.potion, new List<SpawnType>() { SpawnType.W1HPPotion });
    }

    public void SpawnEnemy(UnitSpawnType spawnType,SpawnInfo spawn, RoomScript room)
    {
        var type = spawnType;
        if (possibleEnemies[type].Count == 0)
            type = UnitSpawnType.stupid;

        CreateEnemy(possibleEnemies[type][Mathf.FloorToInt(Random.Range(0, possibleEnemies[type].Count))],new Vector2(spawn.x(), spawn.y()), room);
    }

    private void CreateEnemy(UnitType enemyType, Vector2 pos,RoomScript room)
    {
        IUnit enemy;
        var player = References.instance.UnitHandler.playerController;
        if (enemyType == UnitType.Enemy_Stupid)
            enemy = new Stupid(player);
        else if (enemyType == UnitType.Enemy_Charger)
            enemy = new Charger(player);
        else if (enemyType == UnitType.Enemy_Archer)
            enemy = new Archer(player);
        else if (enemyType == UnitType.Enemy_Boss)
            enemy = new Enemy_Boss();
        else
            throw new System.Exception("enemyType not supported");


        if (enemyType != UnitType.Enemy_Boss)
        {
            References.instance.UIHandler.DebugLogClear();
            References.instance.UIHandler.DebugLog(room.GetChallenge().ToString());
            References.instance.RoomChallengeHandler.ApplyChallenge(enemy, room.GetChallenge());
        }
        enemy.Pos = pos + References.instance.RoomHandler.GetCurrentRoom().GetWorldPos();
        References.instance.RoomHandler.UnitSpawned(enemy);
    }

    public void SpawnPickup(SpawnInfoType spawnType, SpawnInfo spawn)
    {
        var type = spawnType;
        if (possibleRegularSpawns[type].Count == 0)
            type = SpawnInfoType.potion;

        CreateRegularSpawn(possibleRegularSpawns[type][Mathf.FloorToInt(UnityEngine.Random.Range(0, possibleRegularSpawns[type].Count))], new Vector2(spawn.x(), spawn.y()));
    }

    private void CreateRegularSpawn(SpawnType spawnType, Vector2 pos)//Spawns an item based on the enum, this is a 1 to 1
    {
        Position item;
        Vector2 finalPos = pos + References.instance.RoomHandler.GetCurrentRoom().GetWorldPos();
        var player = References.instance.UnitHandler.playerController;
        if (spawnType == SpawnType.W1HPPotion)
            item = new W1HPPot(finalPos, player);
        else if (spawnType == SpawnType.goal)
            item = new GoalScript(finalPos, player);
    }

}
public enum UnitSpawnType { stupid, antiCamp, threat, obstacle, boss }
public enum UnitType { Enemy_Stupid, Enemy_Archer, Enemy_Charger, Enemy_Boss}

public enum SpawnInfoType { potion, aspect, goal}
public enum SpawnType { W1HPPotion, goal}

