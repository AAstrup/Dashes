﻿using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

[System.Serializable]
public class EnemySpawnInfo : Editor_IHasPosition
{
    public float _x;
    public float _y;
    [SerializeField]
    public GroupType _groupType;
    [SerializeField]
    public UnitSpawnType _type;
    public EnemySpawnInfo() { }//Used for serializing
    public EnemySpawnInfo(float x, float y, UnitSpawnType type, GroupType groupNr)
    {
        _x = x;
        _y = y;
        _groupType = groupNr;
        _type = type;
        if (Editor_References.instance != null)
        {//editorMode
            GBRef = Editor_References.instance.CreateGameObject(Editor_References.instance.prefabs.Prefabs["Editor_" + type.ToString()], this);
            SetGMJColor();
        }
    }
    public void LoadSetup()
    {
        GBRef = Editor_References.instance.CreateGameObject(Editor_References.instance.prefabs.Prefabs["Editor_" + _type.ToString()], this);
        SetGMJColor();
    }

    private void SetGMJColor()
    {
        GBRef.GetComponent<SpriteRenderer>().color = GetColor();
    }

    private Color GetColor()
    {
        if (_groupType == GroupType.groupAntiCamp)
            return Color.green;
        else if (_groupType == GroupType.groupHorde)
            return Color.red;
        else if (_groupType == GroupType.groupObstacle)
            return Color.blue;
        else if (_groupType == GroupType.groupThreat)
            return Color.yellow;
        else if (_groupType == GroupType.groupStatic)
            return Color.white;
        else
            throw new Exception("Type Not Supported");
    }

    public UnitSpawnType Type() { return _type; }

    public GroupType GetGroupType() { return _groupType; }

    public float GetX() { return _x; }
    public float GetY() { return _y; }

    public Vector2 GetPosition()
    {
        return new Vector2(GetX(), GetY());
    }

    public GameObject GetGMJ()
    {
        return GBRef;
    }

    [XmlIgnore]
    public GameObject GBRef;
}
public enum GroupType { groupObstacle, groupHorde, groupThreat, groupAntiCamp, groupStatic }//Group static always appears others is randomized
