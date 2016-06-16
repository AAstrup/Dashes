using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressionHandler {

    //Used to generate the a level, increase value to increase differculty
    int level = 1;
    int startRoomsHor = 3;
    int startRoomsVer = 3;
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
        startRoomsHor++;
        startRoomsVer++;
        References.instance.UnitHandler.Reset();
        References.instance.mapGenerator.Reset();
        NewLevel();
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
