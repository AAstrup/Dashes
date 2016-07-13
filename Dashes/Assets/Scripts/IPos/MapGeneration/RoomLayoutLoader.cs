using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

/// <summary>
/// Creates a list of enemyspaws, and regularspawns.
/// </summary>
public class RoomLayoutLoader
{
    string path = "";
    List<RoomLayout> startLoadout = new List<RoomLayout>();
    List<RoomLayout> goalLoadout = new List<RoomLayout>();
    List<RoomLayout> rewardLoadout = new List<RoomLayout>();
    List<RoomLayout> enemyLoadout = new List<RoomLayout>();
    List<RoomLayout> bossLoadout = new List<RoomLayout>();

    Dictionary<RoomScript.roomType, List<RoomLayout>> roomTypeToDictionary;
    Editor_Load loader;

    public void Init()
    {
        //Possible room loadouts are loaded here. Atm we just add some hardcoded by default
        loader = new Editor_Load();
        loader.Init();

        roomTypeToDictionary = new Dictionary<RoomScript.roomType, List<RoomLayout>>();
        roomTypeToDictionary.Add(RoomScript.roomType.B, bossLoadout);
        roomTypeToDictionary.Add(RoomScript.roomType.E, enemyLoadout);
        roomTypeToDictionary.Add(RoomScript.roomType.G, goalLoadout);
        roomTypeToDictionary.Add(RoomScript.roomType.R, rewardLoadout);
        roomTypeToDictionary.Add(RoomScript.roomType.S, startLoadout);

        LoadXMLLayouts();
    }

    private void LoadXMLLayouts()
    {
        string[] paths = Directory.GetFiles(loader.folderPath)
                          .Where(p => p.EndsWith(".xml"))
                          .ToArray();
        foreach (var path in paths)
        {
            var editor_layout = loader.InGame_Load(path);
            var layout = ToNormalLayout(editor_layout);

            roomTypeToDictionary[editor_layout.layoutInfo.roomType].Add(layout);
        }
    }

    private RoomLayout ToNormalLayout(Editor_RoomLayout editor_layout)
    {
        return new RoomLayout(editor_layout.enemieInfos, editor_layout.pickupInfos);
    }

    //Should read from file and spawn upon that.

    public RoomLayout GetStartLoadOut()
    {
        if (startLoadout.Count > 0)
            return startLoadout[UnityEngine.Random.Range(0, startLoadout.Count)];
        else
            return LoadEmptyLoadout();
    }

    public RoomLayout GetGoalLoadOut()
    {
        if (goalLoadout.Count > 0)
            return goalLoadout[UnityEngine.Random.Range(0, goalLoadout.Count)];
        else
            return LoadEmptyLoadout();
    }

    public RoomLayout GetBossLoadOut()
    {
        if (bossLoadout.Count > 0)
            return bossLoadout[UnityEngine.Random.Range(0, goalLoadout.Count)];
        else
            return LoadEmptyLoadout();
    }

    public RoomLayout GetRewardLoadOut()
    {
        if (rewardLoadout.Count > 0)
            return rewardLoadout[UnityEngine.Random.Range(0, rewardLoadout.Count)];
        else
            return LoadEmptyLoadout();
    }

    public RoomLayout GetEnemyLoadout()
    {
        if (enemyLoadout.Count > 0)
            return enemyLoadout[UnityEngine.Random.Range(0, enemyLoadout.Count)];
        else
            return LoadEmptyLoadout();
    }

    public RoomLayout LoadLoadout()
    {
        return null;
    }

    public RoomLayout LoadEmptyLoadout()
    {
        return new RoomLayout(new List<EnemySpawnInfo>(), new List<ItemSpawnInfo>());
    }

}