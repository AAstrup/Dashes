using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoomHandler {

    float timeLastDoorOpened = 0f;
    float doorOpenCD = 0.25f;
    bool doorsUnlocked;
    RoomLayout currentLayout;
    RoomScript currentRoom;
    public List<IUnit> aliveEnemies;
    RoomLayout[,] rooms;//used to get layout of rooms aleredy discovered. Not discovered rooms will be null and generated when needed.

    List<IFactory> factories;

    public void Init(RoomScript startRoom)
    {
        currentRoom = startRoom;
        rooms = new RoomLayout[References.instance.mapGenerator._mapWidth, References.instance.mapGenerator._mapHeight];
        factories = new List<IFactory>();
        factories.Add(new EnemyFactory());
        aliveEnemies = new List<IUnit>();
        EnterRoom(startRoom);
    }

    public void EnterRoom(RoomScript roomScript) {
        timeLastDoorOpened = Time.time;
        References.instance.colSystem.UpdateRoom(roomScript);
        currentRoom = roomScript;
        Debug.Log("Loading with x: " + Mathf.RoundToInt(roomScript._pos.x) + ", y: " + Mathf.RoundToInt(roomScript._pos.y));
        if (rooms[Mathf.RoundToInt(roomScript._pos.x), Mathf.RoundToInt(roomScript._pos.y)] == null)
            currentLayout = References.instance.RoomLayoutHandler.LoadLoadout(currentRoom);
        else
            currentLayout = rooms[Mathf.RoundToInt(roomScript._pos.x), Mathf.RoundToInt(roomScript._pos.y)];

        rooms[Mathf.RoundToInt(roomScript._pos.x), Mathf.RoundToInt(roomScript._pos.y)] = currentLayout;

        References.instance.cameraScript.SetPos(currentRoom);
        List<int> groupNrs = new List<int>() { 1 };
        for (int i = 0; i < factories.Count; i++)
        {
            factories[i].Spawn(currentLayout, groupNrs);
        }
        currentLayout.EnemiesSpawned();
        UpdateDoors();
    }

    public void Update()
    {
        if (!doorsUnlocked)
            return;
        if (Time.time < timeLastDoorOpened + doorOpenCD)
            return;
        float triggerVerDist = 0.5f + 1.5f;
        float triggerHorDist = 1f + 1.5f;
        if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() + new Vector2(0f, currentRoom.GetRoomHeight() / 2f)) < triggerVerDist)//Up
            EnterRoom(References.instance.mapGenerator.GetMap()[Mathf.RoundToInt(currentRoom._pos.x), Mathf.RoundToInt(currentRoom._pos.y + 1)]);
        else if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() - new Vector2(0f, currentRoom.GetRoomHeight() / 2f)) < triggerVerDist)//down
            EnterRoom(References.instance.mapGenerator.GetMap()[Mathf.RoundToInt(currentRoom._pos.x), Mathf.RoundToInt(currentRoom._pos.y - 1)]);
        else if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() - new Vector2(currentRoom.GetRoomWidth() / 2, 0f)) < triggerHorDist)//left
            EnterRoom(References.instance.mapGenerator.GetMap()[Mathf.RoundToInt(currentRoom._pos.x - 1), Mathf.RoundToInt(currentRoom._pos.y)]);
        else if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() + new Vector2(currentRoom.GetRoomWidth() / 2, 0f)) < triggerHorDist)//right
            EnterRoom(References.instance.mapGenerator.GetMap()[Mathf.RoundToInt(currentRoom._pos.x + 1), Mathf.RoundToInt(currentRoom._pos.y)]);

    }

    public void UnitSpawned(IUnit spawnedUnit) {
        aliveEnemies.Add(spawnedUnit); References.instance.UnitHandler.Units.Add(spawnedUnit);
    }
    public void UnitDied(IUnit diedUnit) {
        References.instance.UnitHandler.Units.Remove(diedUnit);
        aliveEnemies.Remove(diedUnit);
        UpdateDoors();
    }

    private void RoomFinished()
    {
        UnlockDoors();
    }

    private void UpdateDoors()
    {
        if (aliveEnemies.Count != 0)
            LockDoors();
        else
            UnlockDoors();
    }

    private void LockDoors()
    {
        doorsUnlocked = false;
        currentRoom.roomStaticGameObjects.LockDoors();
    }

    private void UnlockDoors()
    {
        doorsUnlocked = true;
        currentRoom.roomStaticGameObjects.UnlockDoors();
    }

    public bool DoorsLocked()
    {
        return doorsUnlocked;
    }

    public RoomScript GetCurrentRoom()
    {
        return currentRoom;
    }
}
