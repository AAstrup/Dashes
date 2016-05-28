using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomLayout  {

    public bool hasSpawned = false;
    List<EnemySpawnInfo> _enemies;
    List<PickupSpawnInfo> _pickups;
    public RoomLayout(List<EnemySpawnInfo> enemies, List<PickupSpawnInfo> pickups)
    {
        _enemies = enemies;
        _pickups = pickups;
    }
    public void EnemiesSpawned()
    {
        hasSpawned = true;
    }
    public List<EnemySpawnInfo> GetEnemies()
    {
        hasSpawned = true;
        return _enemies;
    }
    public List<PickupSpawnInfo> GetPickups()
    {
        hasSpawned = true;
        return _pickups;
    }
}
