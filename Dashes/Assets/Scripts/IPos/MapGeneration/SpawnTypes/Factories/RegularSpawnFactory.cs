using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RegularSpawnFactory : IFactory
{
    public void Spawn(RoomLayout layout,List<GroupType> groupNrs,RoomScript room, bool reversePosition)
    {
        if (layout.GetHasSpawned())
            return;

        for (int i = 0; i < layout.GetRegularSpawns().Count; i++)
        {
            if(groupNrs.Contains(layout.GetRegularSpawns()[i].GetGroupType()))
                Spawn(layout.GetRegularSpawns()[i], reversePosition);
        }
    }

    private void Spawn(ItemSpawnInfo info, bool reversePosition)
    {
        References.instance.SpawnHandler.SpawnPickup(info.Type(), info, reversePosition);
    }
}
