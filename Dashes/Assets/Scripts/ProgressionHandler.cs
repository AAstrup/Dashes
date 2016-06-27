using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ProgressionHandler {
    //Statics
    WorldContainerLoader worldLoader;
    WorldContainer currentWorld;
    //Default/start values
    int startWorld = 1;
    int startLevel = 2;

    //Changed when generating new level
    RoomLayoutHandler RoomLayoutHandler;
    SpawnHandler SpawnHandler;
    MapGenerator mapGenerator;
    RoomHandler RoomHandler;

    public void Init()
    {
        worldLoader = new WorldContainerLoader();
        worldLoader.Init();
        currentWorld = worldLoader.GetWorld(startWorld,startLevel);

        NewLevel();
        References.instance.UpdateReferences();
    }

    void NewLevel()
    {
        References.instance.Reset();

        RoomLayoutHandler = new RoomLayoutHandler();//Generates formation of units, COULD be changed
        RoomLayoutHandler.Init();

        SpawnHandler = new SpawnHandler();//Sets the units to spawn when given a unit type
        SpawnHandler.Init(currentWorld.GetSpawnTypeContainer());

        mapGenerator = new MapGenerator(); //Generates the rooms doors and room types
        mapGenerator.Init(currentWorld.GetRoomsHor(), currentWorld.GetRoomsVer(), CalculateTotalRooms(), currentWorld.IsBossLevel());//width,height,maxrooms

        RoomHandler = new RoomHandler();//Keeps track of content in rooms when entering.
        RoomHandler.Init(mapGenerator.GetStartRoom(), mapGenerator, RoomLayoutHandler);

        References.instance.UpdateReferences();
    }

    public void MapComplete()
    {
        currentWorld.RandomMapSizeIncrease();
        currentWorld = worldLoader.IncreaseLevel(currentWorld);

        NewLevel();
    }

    public int CalculateTotalRooms()
    {
        return Mathf.CeilToInt(currentWorld.GetRoomsHor() * currentWorld.GetRoomsHor() * 0.8f);
    }

    public RoomLayoutHandler GetRoomLayoutHandler() { return RoomLayoutHandler; }
    public SpawnHandler GetSpawnHandler() { return SpawnHandler; }
    public MapGenerator GetMapGenerator() { return mapGenerator; }
    public RoomHandler GetRoomHandler() { return RoomHandler; }
    public WorldContainer GetCurrentWorld() { return currentWorld; }
}
