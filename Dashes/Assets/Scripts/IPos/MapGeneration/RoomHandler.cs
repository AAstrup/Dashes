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

    public void Init(RoomScript startRoom)
    {
        currentRoom = startRoom;
        rooms = new RoomLayout[References.instance.mapGenerator._mapWidth, References.instance.mapGenerator._mapHeight];
        factories = new List<IFactory>();
        factories.Add(new EnemyFactory());
        aliveEnemies = new List<IUnit>();
        EnterRoom((int)startRoom._pos.x, (int)startRoom._pos.y);
    }

    public void EnterRoom(int layoutPosX, int layoutPosY) {
        timeLastDoorOpened = Time.time;
        var roomScript = References.instance.mapGenerator.GetMap()[layoutPosX, layoutPosY];
        currentRoom = roomScript;
        References.instance.colSystem.UpdateRoom(roomScript);

        if (rooms[layoutPosX, layoutPosY] == null)
        {
            currentLayout = References.instance.RoomLayoutHandler.LoadLoadout(roomScript);
            currentLayout.hasSpawned = false;
        }
        else
            currentLayout = rooms[layoutPosX, layoutPosY];

        rooms[Mathf.RoundToInt(roomScript._pos.x), Mathf.RoundToInt(roomScript._pos.y)] = currentLayout;

        References.instance.cameraScript.SetPos(currentRoom);
        List<GroupType> groupType = new List<GroupType>() { GroupType.groupAntiCamp, GroupType.groupHorde, GroupType.groupObstacle, GroupType.groupThreat };
        groupType.RemoveAt(Mathf.FloorToInt(Random.Range(0f, groupType.Count)));
        groupType.RemoveAt(Mathf.FloorToInt(Random.Range(0f, groupType.Count)));

        for (int i = 0; i < factories.Count; i++)
        {
            factories[i].Spawn(currentLayout, groupType);
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
            EnterRoom(Mathf.RoundToInt(currentRoom._pos.x), Mathf.RoundToInt(currentRoom._pos.y + 1));
        else if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() - new Vector2(0f, currentRoom.GetRoomHeight() / 2f)) < triggerVerDist)//down
            EnterRoom(Mathf.RoundToInt(currentRoom._pos.x), Mathf.RoundToInt(currentRoom._pos.y - 1));
        else if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() - new Vector2(currentRoom.GetRoomWidth() / 2, 0f)) < triggerHorDist)//left
            EnterRoom(Mathf.RoundToInt(currentRoom._pos.x - 1), Mathf.RoundToInt(currentRoom._pos.y));
        else if (Vector2.Distance(References.instance.UnitHandler.playerIUnit.Pos, currentRoom.GetWorldPos() + new Vector2(currentRoom.GetRoomWidth() / 2, 0f)) < triggerHorDist)//right
            EnterRoom(Mathf.RoundToInt(currentRoom._pos.x + 1), Mathf.RoundToInt(currentRoom._pos.y));

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
