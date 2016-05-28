using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnHandler {
    Dictionary<UnitSpawnType, List<UnitType>> possibleEnemies;
    Dictionary<PickupSpawnType, List<PickupType>> possiblePickup;
    public void Init(List<UnitType> stupids,List<UnitType> antiCamp,List<UnitType> threats,List<UnitType> obstacles)
    {
        possibleEnemies = new Dictionary<UnitSpawnType, List<UnitType>>();
        possibleEnemies.Add(UnitSpawnType.stupid, stupids);
        possibleEnemies.Add(UnitSpawnType.antiCamp, antiCamp);
        possibleEnemies.Add(UnitSpawnType.threat, threats);
        possibleEnemies.Add(UnitSpawnType.obstacle, obstacles);
    }

    public void SpawnEnemy(UnitSpawnType spawnType,SpawnInfo spawn)
    {
        var type = spawnType;
        if (possibleEnemies[type].Count == 0)
            type = UnitSpawnType.stupid;

        CreateEnemy(possibleEnemies[type][Mathf.FloorToInt(Random.Range(0, possibleEnemies[type].Count))],new Vector2(spawn.x(), spawn.y()));
    }

    private void CreateEnemy(UnitType enemyType, Vector2 pos)
    {
        IUnit enemy;
        if (enemyType == UnitType.Enemy_Stupid)
            enemy = new Stupid(References.instance.UnitHandler.playerController);
        else if (enemyType == UnitType.Enemy_Charger)
            enemy = new Charger(References.instance.UnitHandler.playerController);
        else //if (enemyType == IUnitType.Enemy_Archer)
            enemy = new Archer(References.instance.UnitHandler.playerController);

        enemy.Pos = pos + References.instance.RoomHandler.GetCurrentRoom().GetWorldPos();
        References.instance.RoomHandler.UnitSpawned(enemy);
    }

    public void SpawnPickup(PickupSpawnType spawnType, SpawnInfo spawn)
    {
        var type = spawnType;
        if (possiblePickup[type].Count == 0)
            type = PickupSpawnType.health;

        CreatePickup(possiblePickup[type][Mathf.FloorToInt(UnityEngine.Random.Range(0, possiblePickup[type].Count))], new Vector2(spawn.x(), spawn.y()));
    }

    private void CreatePickup(PickupType enemyType, Vector2 pos)
    {
        throw new System.Exception("No pickup created to be spawned!");
        //IUnit enemy;
        //if (enemyType == PickupType.smalHPPotion)
        //    enemy = new Stupid(References.instance.UnitHandler.playerController);
        //else if (enemyType == UnitType.Enemy_Charger)
        //    enemy = new Charger(References.instance.UnitHandler.playerController);
        //else //if (enemyType == IUnitType.Enemy_Archer)
        //    enemy = new Archer(References.instance.UnitHandler.playerController);

        //enemy.Pos = pos + References.instance.RoomHandler.GetCurrentRoom().GetWorldPos();
        //References.instance.RoomHandler.UnitSpawned(enemy);
    }

}
public enum UnitSpawnType { stupid, antiCamp, threat, obstacle }
public enum UnitType { Enemy_Stupid, Enemy_Archer, Enemy_Charger}

public enum PickupSpawnType { health, aspect}
public enum PickupType { smalHPPotion, mediumHPPotion, bigHPPotion, Aspect }

