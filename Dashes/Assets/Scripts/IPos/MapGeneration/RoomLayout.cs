using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoomLayout  {

    bool hasSpawned = false;
    public RoomLayoutOrientation roomOrientation = RoomLayoutOrientation.NotSet;

    public enum RoomLayoutOrientation { NotSet, Vertical, Horizontal, Both }
    List<EnemySpawnInfo> _enemies;
    List<ItemSpawnInfo> _regularSpawns;

    public RoomLayout(List<EnemySpawnInfo> enemies, List<ItemSpawnInfo> pickups)
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
    public List<ItemSpawnInfo> GetRegularSpawns()
    {
        return _regularSpawns;
    }

    public void SetHasSpawned(bool value)
    {
        hasSpawned = value;
    }
}
