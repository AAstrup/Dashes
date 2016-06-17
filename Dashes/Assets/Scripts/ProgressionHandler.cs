﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressionHandler {

    //Used to generate the a level, increase value to increase differculty
    int level = 1;
    int startRoomsHor = 2;
    int startRoomsVer = 2;
    int bossEveryLevelAmount = 3;//This is the last level as well
    int world = 1;//When completing and killing the boss this increases.

    //Changed when generating new level
    RoomLayoutHandler RoomLayoutHandler;
    SpawnHandler SpawnHandler;
    MapGenerator mapGenerator;
    RoomHandler RoomHandler;

    public void Init()
    {
        NewLevel();
        References.instance.UpdateReferences();
    }

    void NewLevel()
    {
        RoomLayoutHandler = new RoomLayoutHandler();//Generates formation of units, COULD be chaned
        RoomLayoutHandler.Init();

        SpawnHandler = new SpawnHandler();//Sets the units to spawn when given a unit type
        SpawnHandler.Init(new List<UnitType> { UnitType.Enemy_Stupid }, new List<UnitType> { UnitType.Enemy_Charger }, new List<UnitType> { UnitType.Enemy_Archer }, new List<UnitType> { });

        mapGenerator = new MapGenerator(); //Generates the rooms doors and room types
        mapGenerator.Init(startRoomsHor, startRoomsVer, CalculateTotalRooms());//width,height,maxrooms

        RoomHandler = new RoomHandler();//Keeps track of content in rooms when entering.
        RoomHandler.Init(mapGenerator.GetStartRoom(), mapGenerator, RoomLayoutHandler);

        References.instance.UpdateReferences();
    }

    public void MapComplete()
    {
        RandomMapSizeIncrease();
        References.instance.UnitHandler.Reset();
        References.instance.DetailHandler.Reset();
        References.instance.mapGenerator.Reset();
        NewLevel();
    }

    private void RandomMapSizeIncrease()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
            startRoomsHor++;
        else if (random == 1)
            startRoomsVer++;
        else
            throw new System.Exception("Someone does Random.range wrong");
    }

    public int CalculateTotalRooms()
    {
        return Mathf.CeilToInt(startRoomsVer * startRoomsHor * 0.8f);
    }

    public RoomLayoutHandler GetRoomLayoutHandler() { return RoomLayoutHandler; }
    public SpawnHandler GetSpawnHandler() { return SpawnHandler; }
    public MapGenerator GetMapGenerator() { return mapGenerator; }
    public RoomHandler GetRoomHandler() { return RoomHandler; }
}