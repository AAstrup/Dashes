using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WorldContainerLoader {

    List<WorldContainer> worldList;

    public void Init()
    {
        worldList = new List<WorldContainer>();
        LoadWorlds();
    }

    private void LoadWorlds()
    {
        //World 0 -> Tutorial
        var spawnListW0 = new List<SpawnTypeContainer>();
        spawnListW0.Add(new SpawnTypeContainer(new List<UnitType>() { UnitType.Enemy_tutorial_Still }, new List<UnitType>() { UnitType.Enemy_tutorial_Towards }, new List<UnitType>() { UnitType.Enemy_tutorial_Flee }));
        var W0 = new WorldContainer(spawnListW0, "Room_TutorialRoomPrefab", 2, 2, false, 0, 1);
        worldList.Add(W0);

        //World 1
        var spawnListW1 = new List<SpawnTypeContainer>();
        spawnListW1.Add(new SpawnTypeContainer(new List<UnitType>() { UnitType.Enemy_Stupid }));
        spawnListW1.Add(new SpawnTypeContainer(new List<UnitType>() { UnitType.Enemy_Stupid }, new List<UnitType>() { }, new List<UnitType>() { UnitType.Enemy_Archer }, new List<UnitType>() { UnitType.Enemy_Waller }));
        spawnListW1.Add(new SpawnTypeContainer(new List<UnitType>() { UnitType.Enemy_Stupid }, new List<UnitType>() { UnitType.Enemy_Charger }, new List<UnitType>() { UnitType.Enemy_Charger }, new List<UnitType>() { UnitType.Enemy_Waller }, new List<UnitType>() { UnitType.Enemy_Boss }));
        var W1 = new WorldContainer(spawnListW1, "Room_GrassRoomPrefab", 2, 2,true, 1, 3);
        worldList.Add(W1);
    }

    public WorldContainer GetWorld(int startWorld,int startLevel = 1)
    {
        worldList[startWorld].SetLevel(startLevel);
        return worldList[startWorld];
    }

    internal WorldContainer IncreaseLevel(WorldContainer currentWorld)
    {
        int level = currentWorld.GetCurrentLevel();
        int maxLevel = currentWorld.GetMaxLevel();
        if((level+1) > maxLevel) {
            return worldList[currentWorld.GetWorldNumber() + 1];
        }
        else
        {
            currentWorld.SetLevel(level+1);
            return currentWorld;
        }
    }
}
