using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Editor_RoomLayout {
    [SerializeField]
    public Editor_RoomLayoutInfo layoutInfo;
    [SerializeField]
    public List<EnemySpawnInfo> enemieInfos;
    [SerializeField]
    public List<ItemSpawnInfo> pickupInfos;

    internal void Init()
    {
        layoutInfo = new Editor_RoomLayoutInfo();
        enemieInfos = new List<EnemySpawnInfo>();
        pickupInfos = new List<ItemSpawnInfo>();

    }
}
