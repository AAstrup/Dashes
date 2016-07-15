using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoomLayout  {

    bool hasSpawned = false;
    public RoomLayoutOrientation _orientation;

    public enum RoomLayoutOrientation { NotSet, Vertical, Horizontal, Both }
    List<EnemySpawnInfo> _enemies;
    List<ItemSpawnInfo> _regularSpawns;

    public RoomLayout(List<EnemySpawnInfo> enemies, List<ItemSpawnInfo> pickups, RoomLayoutOrientation orientation)
    {
        _enemies = enemies;
        _regularSpawns = pickups;
        _orientation = orientation;
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
