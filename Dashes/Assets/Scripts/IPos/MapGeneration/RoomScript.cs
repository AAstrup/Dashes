using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomScript {

    public RoomStaticGameObjects roomStaticGameObjects;
    public float wallHeight = 1f;//Used to create a Collision
    public float wallWidth = 1.75f;//Used to create a Collision
    int arrayHeight = 1;
    int arrayWidth = 1;

    //Used when generating
    public MapGenerator _generator;
    public Vector2 _pos;//Refers to place in the MapGenerators room[,], Not World Position! Call GetWorldPos to read that.
    public int roomLengthNr;
    List<int> possibleDirs = new List<int>();//0 left,1 up, 2 right, 3 down
    //Used to read by when instantiating the room
    public List<int> doors = new List<int>();
    roomType type;

    public RoomScript(Vector2 pos, MapGenerator generator)
    {
        if (pos.x >= 1)
            possibleDirs.Add(0);
        else if (pos.y < generator._mapHeight -1)//-1
            possibleDirs.Add(1);
        else if (pos.x < generator._mapWidth - 1)//-1
            possibleDirs.Add(2);
        else if (pos.y >= 1)
            possibleDirs.Add(3);
        _generator = generator;
        _pos = pos;
        roomLengthNr = 1;
        type = roomType.S;
        _generator.AddActiveRoom(this);
    }
    public RoomScript(RoomScript creator, Vector2 pos, int fromDir)
    {
        possibleDirs.Add(0); possibleDirs.Add(1); possibleDirs.Add(2); possibleDirs.Add(3);
        _generator = creator._generator;
        _pos = pos;
        roomLengthNr = creator.roomLengthNr++;
        possibleDirs.Remove(OppositeDir(fromDir));
        doors.Add(OppositeDir(fromDir));
        _generator.AddActiveRoom(this);
        type = roomType.E;
    }


    int OppositeDir(int dir)
    {
        return Mathf.RoundToInt(Mathf.Repeat(dir + 2, 4));
    }

    Vector2 DirToVector(int dir)
    {
        int x = 0;
        int y = 0;
        if (dir == 0)
            x = -1;
        else if (dir == 1)
            y = 1;
        else if (dir == 2)
            x = 1;
        else if (dir == 3)
            y = -1;

        return new Vector2(x, y);
    }

    public void Expand()
    {
        _generator.RemoveActiveRoom(this);
        bool deadEnd = true;
        int offset = Mathf.FloorToInt(UnityEngine.Random.Range(0, 3));
        int totalDoors = possibleDirs.Count;//Random.Range(1,2);//= Random.Range(1, possibleDirs.Count);
        for (int c = 0; c < Mathf.Min(4, totalDoors); c++)
        {
            if (_generator.HasMoreRooms())
            {
                int d = Mathf.RoundToInt(Mathf.Repeat(c + offset, possibleDirs.Count));
                var point = _pos + DirToVector(possibleDirs[d]);
                if (!_generator.LegitSpawnPoint(point))
                    continue;
                deadEnd = false;
                new RoomScript(this, _pos + DirToVector(possibleDirs[d]), possibleDirs[d]);
                _generator.RoomCreated();
                doors.Add(possibleDirs[d]);
            }
        }
        if (deadEnd)
            DeadEnd();
    }

    void DeadEnd()
    {
        type = roomType.R;
        _generator.AddDeadEnd(this);
    }
    public void SetGoal()
    {
        type = roomType.G;
    }

    public roomType GetRoomType()
    {
        return type;
    }

    public override string ToString()
    {
        return type.ToString();
    }

    public float GetRoomHeight()
    {
        return arrayHeight * _generator.cellHeightRatio;
    }

    public float GetRoomWidth()
    {
        return arrayWidth * _generator.cellWidthRatio;
    }

    public Vector2 GetWorldPos()
    {
        float betweenSpace = _generator.spaceBetweenRooms;
        return new Vector2(_pos.x* (GetRoomWidth() + betweenSpace), _pos.y* (GetRoomHeight() + betweenSpace));
    }

    public bool hasLeftDoor()
    {
        return doors.Contains(0);
    }
    public bool hasTopDoor()
    {
        return doors.Contains(1);
    }
    public bool hasRightDoor()
    {
        return doors.Contains(2);
    }
    public bool hasBottomDoor()
    {
        return doors.Contains(3);
    }
    public enum roomType { S, G, E, R };
}
