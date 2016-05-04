using UnityEngine;
using System.Collections;

public class SpawnInfo {
    float _x;
    float _y;
    int _groupNr;
    public void SetSpawnInfo(float x, float y,int groupNr)
    {
        _x = x;
        _y = y;
        _groupNr = groupNr;
    }
    public float x() { return _x; }
    public float y() { return _y; }
    public int groupNr() { return _groupNr; }
}
