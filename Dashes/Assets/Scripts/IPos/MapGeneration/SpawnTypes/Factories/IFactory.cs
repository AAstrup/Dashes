﻿using UnityEngine;
using System.Collections.Generic;

public interface IFactory {

    void Spawn(RoomLayout layout, List<GroupType> groupNrs, RoomScript room,Vector2 reversePosition);
}
