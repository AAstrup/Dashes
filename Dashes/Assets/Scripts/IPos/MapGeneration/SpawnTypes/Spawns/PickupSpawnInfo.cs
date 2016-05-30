using UnityEngine;
using System.Collections;

[System.Serializable]
public class PickupSpawnInfo : SpawnInfo
{
    public PickupSpawnInfo(float x, float y, PickupSpawnType type, GroupType groupNr)
    {
        SetSpawnInfo(x, y, groupNr);
        _type = type;
    }
    public PickupSpawnType Type() { return _type; }
    PickupSpawnType _type;
}