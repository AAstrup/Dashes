using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnHandler {
    Dictionary<UnitSpawnType, List<UnitType>> possibleEnemies;
    Dictionary<SpawnInfoType, List<SpawnType>> possibleRegularSpawns;
    public void Init(SpawnTypeContainer spawnTypes)
    {
        possibleEnemies = new Dictionary<UnitSpawnType, List<UnitType>>();
        possibleEnemies.Add(UnitSpawnType.stupid, spawnTypes._stupids);
        possibleEnemies.Add(UnitSpawnType.antiCamp, spawnTypes._antiCamp);
        possibleEnemies.Add(UnitSpawnType.threat, spawnTypes._threats);
        possibleEnemies.Add(UnitSpawnType.obstacle, spawnTypes._obstacles);
        possibleEnemies.Add(UnitSpawnType.boss, spawnTypes._boss);

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
        else if (enemyType == UnitType.Enemy_Waller)
            enemy = new Enemy_Waller(player);
        else if (enemyType == UnitType.Enemy_Archer)
            enemy = new Archer(player);
        else if (enemyType == UnitType.Enemy_Boss)
            enemy = new Enemy_Boss();
        else if (enemyType == UnitType.Enemy_tutorial_Flee)
            enemy = new Enemy_tutorial_Flee(player);
        else if (enemyType == UnitType.Enemy_tutorial_Still)
            enemy = new Enemy_tutorial_Still(player);
        else if (enemyType == UnitType.Enemy_tutorial_Towards)
            enemy = new Enemy_tutorial_Towards(player);
        else if (enemyType == UnitType.Enemy_Blob)
            enemy = new Enemy_Blob2(player);
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
public enum UnitSpawnType {
    stupid,     //There can be a lot of these without making it impossible. They serve as a tool for the player to move fast.
    antiCamp,   //They get close to the player in order to prevent camping. They are not a big threat
    threat,     //Ranged and possea threat. They are top priority to kill.
    obstacle,   //They prevents the player for jumping right to threats. 
    boss
}
public enum UnitType {
    //Introduced at World 0
    Enemy_tutorial_Towards, Enemy_tutorial_Still, Enemy_tutorial_Flee, Enemy_tutorial_BossSpawner,
    //Introduced at world 1
    Enemy_Stupid, Enemy_Archer, Enemy_Charger, Enemy_Waller, Enemy_Boss,       
    //Introduced at world 2
    Enemy_Blob, Enemy_Reviver     
}

public enum SpawnInfoType { potion, aspect, goal}
public enum SpawnType { W1HPPotion, goal}

