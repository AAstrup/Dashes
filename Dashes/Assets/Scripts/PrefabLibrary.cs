using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PrefabLibrary
{

    public Dictionary<string,GameObject> Prefabs; 

    public void Init()
    {
        Prefabs = new Dictionary<string, GameObject>();

        foreach (GameObject pre in Resources.LoadAll("Prefabs", typeof(GameObject)))
        {
            Prefabs.Add(pre.name, pre);
        }

    }

}
