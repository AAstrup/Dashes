using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomLayout  {

    public bool enemiesSpawned = false;
    public List<EnemySpawnInfo> _enemies;
    public RoomLayout(List<EnemySpawnInfo> enemies)
    {
        _enemies = enemies;
    }
    public void EnemiesSpawned()
    {
        enemiesSpawned = true;
    }
}
