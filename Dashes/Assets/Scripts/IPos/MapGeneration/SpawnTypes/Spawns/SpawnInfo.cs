using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnInfo {
    float _x;
    float _y;
    GroupType _groupType;
    public SpawnInfo(float x, float y, SpawnInfoType type,GroupType groupType)
    {
        _type = type;
        SetSpawnInfo(x, y, groupType);
    }
    public SpawnInfo(int DONOTCALLTHIS) { }//USED ONLY FOR INHEIRITING
    public SpawnInfoType Type() { return _type; }
    SpawnInfoType _type;

    public void SetSpawnInfo(float x, float y, GroupType groupNr)
    {
        _x = x;
        _y = y;
        _groupType = groupNr;
    }
    public float x() { return _x; }
    public float y() { return _y; }
    public GroupType GetGroupType() { return _groupType; }

}
public enum GroupType { groupObstacle,groupHorde,groupThreat,groupAntiCamp, groupStatic}//Group static always appears others is randomized