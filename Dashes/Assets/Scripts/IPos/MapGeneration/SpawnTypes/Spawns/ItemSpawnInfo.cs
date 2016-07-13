using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

public class ItemSpawnInfo : Editor_IHasPosition
{
    public float _x;
    public float _y;
    public GroupType _groupType;

    public ItemSpawnInfo() { }//Used for serializing
    public ItemSpawnInfo(float x, float y, SpawnInfoType type, GroupType groupNr)
    {
        _x = x;
        _y = y;
        _groupType = groupNr;
        _type = type;
        if(Editor_References.instance != null)//editorMode
            GBRef = Editor_References.instance.CreateGameObject( Editor_References.instance.prefabs.Prefabs["Editor_" + type.ToString()],this);
    }

    public SpawnInfoType Type() { return _type; }
    [SerializeField]
    SpawnInfoType _type;
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

    public void LoadSetup()
    {
        GBRef = Editor_References.instance.CreateGameObject(Editor_References.instance.prefabs.Prefabs["Editor_" + _type.ToString()], this);
    }
}
