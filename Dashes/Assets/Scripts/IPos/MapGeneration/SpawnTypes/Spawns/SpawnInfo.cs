using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnInfo {
    float _x;
    float _y;
    GroupType _groupNr;
    public void SetSpawnInfo(float x, float y, GroupType groupNr)
    {
        _x = x;
        _y = y;
        _groupNr = groupNr;
    }
    public float x() { return _x; }
    public float y() { return _y; }
    public GroupType groupNr() { return _groupNr; }
}
public enum GroupType { groupObstacle,groupHorde,groupThreat,groupAntiCamp}