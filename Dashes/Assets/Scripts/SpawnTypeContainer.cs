using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnTypeContainer {
    public List<UnitType> _stupids;
    public List<UnitType> _antiCamp;
    public List<UnitType> _threats;
    public List<UnitType> _obstacles;
    public List<UnitType> _boss;

    public SpawnTypeContainer (List<UnitType> stupids = null, List<UnitType> antiCamp = null, List<UnitType> threats = null, List<UnitType> obstacles = null, List<UnitType> boss = null){
        _stupids = GetList(stupids);
        _antiCamp = GetList(antiCamp);
        _threats = GetList(threats);
        _obstacles = GetList(obstacles);
        _boss = GetList(boss);
    }


    List<UnitType> GetList(List<UnitType> listOption)
    {
        if (listOption == null)
            return new List<UnitType>();
        else
            return listOption;
    }
}

