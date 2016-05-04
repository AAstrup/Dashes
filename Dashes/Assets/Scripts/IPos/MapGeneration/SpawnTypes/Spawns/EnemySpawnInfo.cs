using UnityEngine;
using System.Collections;

public class EnemySpawnInfo : SpawnInfo {
    public EnemySpawnInfo(float x, float y, EnemyType type,int groupNr)
    {
        SetSpawnInfo(x, y, groupNr);
        _type = type;
    }
    public EnemyType Type() { return _type; }
    EnemyType _type;
}