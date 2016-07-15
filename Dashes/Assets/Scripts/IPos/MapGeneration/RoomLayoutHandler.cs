using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoomLayoutHandler {

    RoomLayoutLoader loader;

    public void Init()
    {
        loader = new RoomLayoutLoader();
        loader.Init();
    }

    public RoomLayout LoadLoadout(RoomScript roomScript, RoomLayout.RoomLayoutOrientation orientation)
    {
        if (roomScript.GetRoomType() == RoomScript.roomType.E)
            return LoadEnemyLayout(orientation);
        else if (roomScript.GetRoomType() == RoomScript.roomType.R)
            return LoadRewardLayout(orientation);
        else if (roomScript.GetRoomType() == RoomScript.roomType.G)
            return LoadGoalLayout(orientation);
        else if (roomScript.GetRoomType() == RoomScript.roomType.S)
            return LoadStartLayout(orientation);
        else if (roomScript.GetRoomType() == RoomScript.roomType.B)
            return LoadBossLayout(orientation);
        else
            throw new Exception("BAD ROOM TYPE");
    }

    private RoomLayout LoadBossLayout(RoomLayout.RoomLayoutOrientation orientation)
    {
        return loader.GetBossLoadOut(orientation);
    }

    private RoomLayout LoadStartLayout(RoomLayout.RoomLayoutOrientation orientation)
    {
        return loader.GetStartLoadOut(orientation);
    }

    private RoomLayout LoadGoalLayout(RoomLayout.RoomLayoutOrientation orientation)
    {
        return loader.GetGoalLoadOut(orientation);
    }

    private RoomLayout LoadRewardLayout(RoomLayout.RoomLayoutOrientation orientation)
    {
        return loader.GetRewardLoadOut(orientation);
    }

    public RoomLayout LoadEnemyLayout(RoomLayout.RoomLayoutOrientation orientation)
    {
        return loader.GetEnemyLoadout(orientation);
    }
}
