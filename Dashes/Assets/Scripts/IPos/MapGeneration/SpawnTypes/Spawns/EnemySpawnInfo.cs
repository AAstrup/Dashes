using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemySpawnInfo : SpawnInfo {
    public EnemySpawnInfo(float x, float y, UnitSpawnType type,int groupNr)
    {
        SetSpawnInfo(x, y, groupNr);
        _type = type;
    }
    public UnitSpawnType Type() { return _type; }
    UnitSpawnType _type;
}