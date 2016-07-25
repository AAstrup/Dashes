﻿using System.ComponentModel;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomHandler {

    float timeLastDoorOpened = 0f;
    float doorOpenCD = 0.25f;
    bool doorsUnlocked;
    RoomLayout currentLayout;
    RoomScript currentRoom;
    public List<IUnit> aliveEnemies;
    RoomLayout[,] rooms;//used to get layout of rooms aleredy discovered. Not discovered rooms will be null and generated when needed.

    List<IFactory> factories;

    public void Init(RoomScript startRoom,MapGenerator mapGenerator, RoomLayoutHandler roomLayoutHandler)
    {
        currentRoom = startRoom;
        rooms = new RoomLayout[mapGenerator._mapWidth, mapGenerator._mapHeight];
        factories = new List<IFactory>();
        factories.Add(new EnemyFactory());
        factories.Add(new RegularSpawnFactory());
        aliveEnemies = new List<IUnit>();
        EnterRoom((int)startRoom._pos.x, (int)startRoom._pos.y,mapGenerator,roomLayoutHandler);
    }

    public void EnterRoom(int layoutPosX, int layoutPosY, MapGenerator mapGenerator, RoomLayoutHandler roomLayoutHandler) {
        timeLastDoorOpened = Time.time;
        var roomScript = mapGenerator.GetMap()[layoutPosX, layoutPosY];

        if (References.instance.MapUnknown)
        {
            References.instance.UIHandler.MapEnableRoom(layoutPosX, layoutPosY);
        }
        References.instance.UIHandler.MapUpdate(layoutPosX,layoutPosY);

        currentRoom = roomScript;
        References.instance.colSystem.UpdateRoom(roomScript);

        if (rooms[layoutPosX, layoutPosY] == null)
        {
            currentLayout = roomLayoutHandler.LoadLoadout(roomScript);
            currentLayout.SetHasSpawned(false);
        }
        else
            currentLayout = rooms[layoutPosX, layoutPosY];

        rooms[Mathf.RoundToInt(roomScript._pos.x), Mathf.RoundToInt(roomScript._pos.y)] = currentLayout;

        References.instance.cameraScript.SetPos(currentRoom,false);
        List<GroupType> groupType = new List<GroupType>() { GroupType.groupAntiCamp, GroupType.groupHorde, GroupType.groupObstacle, GroupType.groupThreat };
        groupType.RemoveAt(Mathf.FloorToInt(Random.Range(0f, groupType.Count)));
        groupType.RemoveAt(Mathf.FloorToInt(Random.Range(0f, groupType.Count)));
        groupType.Add(GroupType.groupStatic);

        for (int i = 0; i < factories.Count; i++)
        {
            factories[i].Spawn(currentLayout, groupType, currentRoom);
        }
        currentLayout.SetHasSpawned(true);
        UpdateDoors();

        Time.timeScale = 0f;//Pauses the game when the room is loaded

    }

    public void Update()
    {
        if (!doorsUnlocked)
            return;
        if (Time.time < timeLastDoorOpened + doorOpenCD)
            return;
        float triggerVerDist = 0.5f + 1.5f;
        float triggerHorDist = 1f + 1.5f;
        if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() + new Vector2(0f, currentRoom.GetRoomHeight() / 2f)) < triggerVerDist)
        {//Up
            if(currentRoom.hasTopDoor())
                EnterRoom(Mathf.RoundToInt(currentRoom._pos.x), Mathf.RoundToInt(currentRoom._pos.y + 1),References.instance.mapGenerator,References.instance.RoomLayoutHandler);
        }
        else if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() - new Vector2(0f, currentRoom.GetRoomHeight() / 2f)) < triggerVerDist)
        {//down
            if (currentRoom.hasBottomDoor())
                EnterRoom(Mathf.RoundToInt(currentRoom._pos.x), Mathf.RoundToInt(currentRoom._pos.y - 1), References.instance.mapGenerator, References.instance.RoomLayoutHandler);
        }
        else if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() - new Vector2(currentRoom.GetRoomWidth() / 2, 0f)) < triggerHorDist)
        {//left
            if (currentRoom.hasLeftDoor())
                EnterRoom(Mathf.RoundToInt(currentRoom._pos.x - 1), Mathf.RoundToInt(currentRoom._pos.y), References.instance.mapGenerator, References.instance.RoomLayoutHandler);
        }
        else if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() + new Vector2(currentRoom.GetRoomWidth() / 2, 0f)) < triggerHorDist)
        {//right
            if (currentRoom.hasRightDoor())
                EnterRoom(Mathf.RoundToInt(currentRoom._pos.x + 1), Mathf.RoundToInt(currentRoom._pos.y), References.instance.mapGenerator, References.instance.RoomLayoutHandler);
        }
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
