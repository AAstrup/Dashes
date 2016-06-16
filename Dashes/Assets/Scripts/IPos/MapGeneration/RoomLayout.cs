using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoomLayout  {

    bool hasSpawned = false;
    List<EnemySpawnInfo> _enemies;
    List<SpawnInfo> _regularSpawns;
    public RoomLayout(List<EnemySpawnInfo> enemies, List<SpawnInfo> pickups)
    {
        _enemies = enemies;
        _regularSpawns = pickups;
    }
    public bool GetHasSpawned()
    {
        return hasSpawned;
    }
    public List<EnemySpawnInfo> GetEnemies()
    {
        return _enemies;
    }
    public List<SpawnInfo> GetRegularSpawns()
    {
        return _regularSpawns;
    }

    public void SetHasSpawned(bool value)
    {
        hasSpawned = value;
    }
}
