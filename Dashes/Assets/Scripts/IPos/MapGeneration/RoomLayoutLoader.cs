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
    RoomLayoutContainer startLoadout = new RoomLayoutContainer();
    RoomLayoutContainer goalLoadout = new RoomLayoutContainer();
    RoomLayoutContainer rewardLoadout = new RoomLayoutContainer();
    RoomLayoutContainer enemyLoadout = new RoomLayoutContainer();
    RoomLayoutContainer bossLoadout = new RoomLayoutContainer();

    Dictionary<RoomScript.roomType, RoomLayoutContainer> roomTypeToDictionary;
    Editor_Load loader;

    public void Init()
    {
        //Possible room loadouts are loaded here. Atm we just add some hardcoded by default
        loader = new Editor_Load();
        loader.Init();

        roomTypeToDictionary = new Dictionary<RoomScript.roomType, RoomLayoutContainer>();
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

    public RoomLayout GetStartLoadOut(RoomLayout.RoomLayoutOrientation orientation)
    {
        if (startLoadout.both.Count > 0)
            return startLoadout.GetRandomRoom(orientation);
        else
            return LoadEmptyLoadout();
    }

    public RoomLayout GetGoalLoadOut(RoomLayout.RoomLayoutOrientation orientation)
    {
        if (goalLoadout.both.Count > 0)
            return goalLoadout.GetRandomRoom(orientation);
        else
            return LoadEmptyLoadout();
    }

    public RoomLayout GetBossLoadOut(RoomLayout.RoomLayoutOrientation orientation)
    {
        if (bossLoadout.both.Count > 0)
            return bossLoadout.GetRandomRoom(orientation);
        else
            return LoadEmptyLoadout();
    }

    public RoomLayout GetRewardLoadOut(RoomLayout.RoomLayoutOrientation orientation)
    {
        if (rewardLoadout.both.Count > 0)
            return rewardLoadout.GetRandomRoom(orientation);
        else
            return LoadEmptyLoadout();
    }

    public RoomLayout GetEnemyLoadout(RoomLayout.RoomLayoutOrientation orientation)
    {
        if (enemyLoadout.both.Count > 0)
            return enemyLoadout.GetRandomRoom(orientation);
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

    class RoomLayoutContainer{
        public List<RoomLayout> vertical;
        public List<RoomLayout> horizontal;
        public List<RoomLayout> both;
        public RoomLayoutContainer()
        {
            vertical = new List<RoomLayout>();
            horizontal = new List<RoomLayout>();
            both = new List<RoomLayout>();
        }
        public void Add(RoomLayout layout)
        {
            if (layout.roomOrientation == RoomLayout.RoomLayoutOrientation.NotSet)
                throw new Exception("A room has not set its orientation");
            if (layout.roomOrientation == RoomLayout.RoomLayoutOrientation.Vertical)
                vertical.Add(layout);
            else if (layout.roomOrientation == RoomLayout.RoomLayoutOrientation.Horizontal)
                horizontal.Add(layout);
            both.Add(layout);
        }

        internal RoomLayout GetRandomRoom(RoomLayout.RoomLayoutOrientation orientation)
        {
            if(orientation == RoomLayout.RoomLayoutOrientation.Horizontal)
                return horizontal[UnityEngine.Random.Range(0, horizontal.Count)];
            else if (orientation == RoomLayout.RoomLayoutOrientation.Vertical)
                return vertical[UnityEngine.Random.Range(0, vertical.Count)];
            else
                return both[UnityEngine.Random.Range(0, both.Count)];
        }
    }
}