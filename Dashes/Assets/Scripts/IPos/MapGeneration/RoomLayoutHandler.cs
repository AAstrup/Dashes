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

    public RoomLayout LoadLoadout(RoomScript roomScript)
    {
        if (roomScript.GetRoomType() == RoomScript.roomType.E)
            return LoadEnemyLayout();
        if (roomScript.GetRoomType() == RoomScript.roomType.R)
            return LoadRewardLayout();
        if (roomScript.GetRoomType() == RoomScript.roomType.G)
            return LoadGoalLayout();
        else if (roomScript.GetRoomType() == RoomScript.roomType.S)
            return LoadStartLayout();
        else
            throw new Exception("BAD ROOM TYPE");
    }

    private RoomLayout LoadStartLayout()
    {
        return loader.GetStartLoadOut();
    }

    private RoomLayout LoadGoalLayout()
    {
        return loader.GetGoalLoadOut();
    }

    private RoomLayout LoadRewardLayout()
    {
        return loader.GetRewardLoadOut();
    }

    public RoomLayout LoadEnemyLayout()
    {
        return loader.GetEnemyLoadOut();
    }
}
