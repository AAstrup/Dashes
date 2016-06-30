using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator {

    RoomScript startRoom;//used in references to Setup RoomHandler
    RoomScript[,] rooms;
    List<RoomScript> activeRooms = new List<RoomScript>();
    List<RoomScript> deadEnds = new List<RoomScript>();
    public int _mapWidth;
    public int _mapHeight;
    int _roomsLeft;
    public float spaceBetweenRooms = 1f;
    public float cellHeightRatio = 10f;//one cell in the array rooms(RoomScript[,]) has a height of heightRatio
    public float cellWidthRatio = 16f;//one cell in the array rooms(RoomScript[,]) has a width of widthRatio
    bool _isBossLevel;

    public void Init(int mapWidth,int mapHeight,int roomsLeft,bool isBossLevel)
    {
        _isBossLevel = isBossLevel;
        _mapHeight = mapHeight;
        _mapWidth = mapWidth;
        _roomsLeft = roomsLeft;
        rooms = new RoomScript[mapWidth, mapHeight];
        CreateLevel();
        RoomScript startRoom = GetStartRoom();
        var StartPos = startRoom.GetWorldPos();
        References.instance.UnitHandler.playerIUnit.Pos = StartPos;
        References.instance.cameraScript.SetPos(startRoom);
    }

    void CreateLevel()
    {
        Vector2 StartPos = new Vector2(Mathf.FloorToInt(Random.Range(0f, _mapWidth)), Mathf.FloorToInt(Random.Range(0f, _mapHeight)));
        new RoomScript(StartPos, this);
        while (activeRooms.Count > 0)
        {
            int index = Mathf.FloorToInt(Random.Range(0, activeRooms.Count));
            activeRooms[index].Expand();
        }

        RoomScript goal = null;
        int length = 0;
        for (int r = 0; r < deadEnds.Count; r++)
        {
            if (deadEnds[r].roomLengthNr > length)
            {
                length = deadEnds[r].roomLengthNr;
                goal = deadEnds[r];
            }
        }
        goal.SetGoal(_isBossLevel);
        printMap();
        DrawMap();

        
    }

    public void Reset()
    {
        for (int y = 0; y < rooms.GetLength(1); y++)
        {
            for (int x = 0; x < rooms.GetLength(0); x++)
            {
                if (rooms[x, y] != null)
                    rooms[x, y].roomStaticGameObjects.Reset();
            }
        }
    }

    private void DrawMap()
    {
        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                if (rooms[x, y] != null)
                {
                    var staticGmj = References.instance.CreateRoomPrefab(rooms[x, y], x, y);
                    rooms[x, y].roomStaticGameObjects = staticGmj;
                }
            }
        }
    }

    void printMap()
    {
        for (int y = _mapHeight-1; y >= 0; y--)
        {
            string toPrint = " ";
            for (int x = 0; x < _mapWidth; x++)
            {
                if (rooms[x, y] != null)
                    toPrint += rooms[x, y].ToString();
                else
                    toPrint += "_";
                toPrint += " ";
            }
        }
    }

    public bool LegitSpawnPoint(Vector2 pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= _mapWidth || pos.y >= _mapHeight)
            return false;
        if (rooms[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)] != null)
            return false;
        return true;
    }

    public void AddActiveRoom(RoomScript room)
    {
        activeRooms.Add(room);
        Vector2 pos = new Vector2(room._pos.x, room._pos.y);//As an array 0,0 is the bottom the pos has to be inverted in y-axis
        rooms[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)] = room;
    }
    public void RemoveActiveRoom(RoomScript room)
    {
        activeRooms.Remove(room);
    }
    public void AddDeadEnd(RoomScript room)
    {
        deadEnds.Add(room);
    }
    
    public bool HasMoreRooms()
    {
        return _roomsLeft > 0;
    }
    public void RoomCreated()
    {
        _roomsLeft--;
    }

    public RoomScript GetStartRoom()
    {
        //rooms
        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                if (rooms[x, y] == null)
                    continue;
                if (rooms[x, y].GetRoomType() == RoomScript.roomType.S)
                    return rooms[x, y];
            }
        }
        throw new System.Exception("START ROOM NOT FOUND!?");
    }

    public RoomScript[,] GetMap() { return rooms; }
}
