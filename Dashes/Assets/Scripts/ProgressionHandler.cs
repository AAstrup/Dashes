using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ProgressionHandler {
    //Statics
    SpawnTypeContainerLoader loader;
    //Used to generate the a level, increase value to increase differculty
    int level = 1;
    int startRoomsHor = 2;
    int startRoomsVer = 2;
    int bossEveryLevelAmount = 2;//Every this value of levels a boss will spawn instead the goal
    int world = 1;//When completing and killing the boss this increases.

    //Changed when generating new level
    RoomLayoutHandler RoomLayoutHandler;
    SpawnHandler SpawnHandler;
    MapGenerator mapGenerator;
    RoomHandler RoomHandler;

    public void Init()
    {
        loader = new SpawnTypeContainerLoader();
        loader.Init();

        NewLevel();
        References.instance.UpdateReferences();
    }

    void NewLevel()
    {
        RoomLayoutHandler = new RoomLayoutHandler();//Generates formation of units, COULD be chaned
        RoomLayoutHandler.Init();

        SpawnHandler = new SpawnHandler();//Sets the units to spawn when given a unit type
        SpawnHandler.Init(loader.LoadSpawnTypeContainer(level,world));

        mapGenerator = new MapGenerator(); //Generates the rooms doors and room types
        mapGenerator.Init(startRoomsHor, startRoomsVer, CalculateTotalRooms(), IsBossLevel());//width,height,maxrooms

        RoomHandler = new RoomHandler();//Keeps track of content in rooms when entering.
        RoomHandler.Init(mapGenerator.GetStartRoom(), mapGenerator, RoomLayoutHandler);

        References.instance.UpdateReferences();

        References.instance.UIHandler.UpdateLevel(world,level);
    }

    private bool IsBossLevel()
    {
        var debug = level == bossEveryLevelAmount;
        return debug;
    }

    public void MapComplete()
    {
        RandomMapSizeIncrease();
        Debug.Log("level is " + level + ", boss is " + bossEveryLevelAmount + " result is " + IsBossLevel());
        References.instance.UnitHandler.Reset();
        References.instance.DetailHandler.Reset();
        References.instance.triggerHandler.Reset();
        References.instance.mapGenerator.Reset();
        if (IsBossLevel())
        {
            level = 1;
            world++;
        }
        else
        {
            level++;
        }

        NewLevel();
    }

    private void RandomMapSizeIncrease()
    {
        int random = UnityEngine.Random.Range(0, 2);
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
