using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Editor_UIEvents : MonoBehaviour {


    public Dictionary<GroupType, Block> groupTypeToBlock;
    public Dictionary<UnitSpawnType, int> unitTypeCost;
    public Dictionary<SpawnInfoType, int> itemTypeCost;
    public Text[] texts;
    void Start()
    {
        unitTypeCost = new Dictionary<UnitSpawnType, int>();
        unitTypeCost.Add(UnitSpawnType.antiCamp, 3);
        unitTypeCost.Add(UnitSpawnType.obstacle, 2);
        unitTypeCost.Add(UnitSpawnType.stupid, 1);
        unitTypeCost.Add(UnitSpawnType.threat, 3);
        unitTypeCost.Add(UnitSpawnType.boss, 20);

        itemTypeCost = new Dictionary<SpawnInfoType, int>();
        itemTypeCost.Add(SpawnInfoType.aspect, 4);
        itemTypeCost.Add(SpawnInfoType.potion, 1);
        itemTypeCost.Add(SpawnInfoType.goal, 999);

        Reset();
    }

    public void Reset()
    {
        groupTypeToBlock = new Dictionary<GroupType, Block>();
        groupTypeToBlock.Add(GroupType.groupStatic, new Block(0, texts[0]));
        groupTypeToBlock.Add(GroupType.groupThreat, new Block(0, texts[1]));
        groupTypeToBlock.Add(GroupType.groupObstacle, new Block(0, texts[2]));
        groupTypeToBlock.Add(GroupType.groupAntiCamp, new Block(0, texts[3]));
        groupTypeToBlock.Add(GroupType.groupHorde, new Block(0, texts[4]));

    }
    public InputField inputField;
    public void SelectGroupType(string groupType) {
        Editor_References.instance.drawer.groupType = (GroupType) Enum.Parse(typeof (GroupType), groupType);
    }
    public void SelectEnemyType(string enemyType) {
        Editor_References.instance.drawer.unitType = (UnitSpawnType)Enum.Parse(typeof(UnitSpawnType), enemyType);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Editor_References.instance.drawer.Draw(GetMouseWorldPosition());
        else if (Input.GetMouseButtonDown(1))
            Editor_References.instance.drawer.Delete(GetMouseWorldPosition());
    }

    Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    public class Block
    {
        public Block(int value,Text text)
        {
            _value = value;
            _text = text;
            UpdateText();
        }
        public void Increase(int v)
        {
            _value += v;
            UpdateText();
        }

        private void UpdateText()
        {
            _text.text = _value.ToString();
        }

        int _value;
        Text _text;
    }
}
