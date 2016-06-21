using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class SpawnTypeContainerLoader  {
    
    List<Dictionary<int, SpawnTypeContainer>> worldsContainer;

    public void Init()
    {
        worldsContainer = new List<Dictionary<int, SpawnTypeContainer>>();
        LoadContainers();
    }

    public SpawnTypeContainer LoadSpawnTypeContainer(int level,int world)
    {
        return worldsContainer[world][level];
    }

    void LoadContainers()
    {
        var LevelToSpawnContainer = new Dictionary<int, SpawnTypeContainer>();
        //World 0 -> Tutorial
        LevelToSpawnContainer = new Dictionary<int, SpawnTypeContainer>();
        LevelToSpawnContainer.Add(1, new SpawnTypeContainer(new List<UnitType>() { UnitType.Enemy_tutorial_Still },null,null,null, new List<UnitType>() { UnitType.Enemy_tutorial_BossSpawner }));
        LevelToSpawnContainer.Add(2, new SpawnTypeContainer(new List<UnitType>() { UnitType.Enemy_tutorial_Still }, new List<UnitType>() { UnitType.Enemy_tutorial_Towards }, new List<UnitType>() { UnitType.Enemy_tutorial_Flee },null, new List<UnitType>() { UnitType.Enemy_tutorial_BossSpawner }));
        LevelToSpawnContainer.Add(3, new SpawnTypeContainer(new List<UnitType>() { UnitType.Enemy_tutorial_Towards }, null, new List<UnitType>() { UnitType.Enemy_tutorial_Flee },null, new List<UnitType>() { UnitType.Enemy_tutorial_BossSpawner}));
        worldsContainer.Add(LevelToSpawnContainer);
        //World 1
        LevelToSpawnContainer = new Dictionary<int, SpawnTypeContainer>();
        LevelToSpawnContainer.Add(1, new SpawnTypeContainer(new List<UnitType>() { UnitType.Enemy_Stupid }));
        LevelToSpawnContainer.Add(2, new SpawnTypeContainer(new List<UnitType>() { UnitType.Enemy_Stupid }, new List<UnitType>() { },new List<UnitType>() { UnitType.Enemy_Archer }));
        LevelToSpawnContainer.Add(3, new SpawnTypeContainer(new List<UnitType>() { UnitType.Enemy_Stupid }, new List<UnitType>() { UnitType.Enemy_Charger }, new List<UnitType>() { UnitType.Enemy_Charger },null,new List<UnitType>() { UnitType.Enemy_Boss }));
        worldsContainer.Add(LevelToSpawnContainer);
    }
}
