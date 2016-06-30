using UnityEngine;
using System.Collections.Generic;

public class WorldContainer {

    int _currentLevel;
    int _worldNr;
    int _maxLevel;
    bool _hasBoss;
    string _roomPrefabName;

    List<SpawnTypeContainer> _spawnTypeContainers;

    public int RoomsHor { get; private set; }
    public int RoomsVer { get; private set; }

    public WorldContainer(List<SpawnTypeContainer> spawnTypeContainers,string roomPrefabName, int StartRoomsHor,int StartRoomsVer, bool hasBoss, int worldNr, int maxLevel, int currentLevel = 1)
    {
        _spawnTypeContainers = spawnTypeContainers;
        _roomPrefabName = roomPrefabName;
        _currentLevel = currentLevel;
        _worldNr = worldNr;
        _maxLevel = maxLevel;
        _hasBoss = hasBoss;
        RoomsHor = StartRoomsHor;
        RoomsVer = StartRoomsVer;

        if (spawnTypeContainers.Count != maxLevel)
            throw new System.Exception("Maxlevel and spawnTypes does not match!");
    }

    internal SpawnTypeContainer GetSpawnTypeContainer()
    {
        return _spawnTypeContainers[_currentLevel-1];
    }

    internal int GetRoomsHor()
    {
        return RoomsHor;
    }

    internal int GetCurrentLevel()
    {
        return _currentLevel;
    }

    internal int GetMaxLevel()
    {
        return _maxLevel;
    }

    internal int GetWorldNumber()
    {
        return _worldNr;
    }

    internal void SetLevel(int v)
    {
        _currentLevel = v;
    }
    
    internal int GetRoomsVer()
    {
        return RoomsVer;
    }

    internal bool IsBossLevel()
    {
        return (_currentLevel == _maxLevel && _hasBoss);
    }

    public void RandomMapSizeIncrease()
    {
        int random = UnityEngine.Random.Range(0, 2);
        if (random == 0)
            RoomsHor++;
        else if (random == 1)
            RoomsVer++;
        else
            throw new System.Exception("Someone does Random.range wrong");
    }
    public string GetRoomPrefabName() { return _roomPrefabName; }
}
