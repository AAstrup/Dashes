using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DetailHandler {

    List<GameObject> details;
    List<GameObject> triggers;

    public void AddDetail(GameObject gmj)
    {
        details.Add(gmj);
    }
    public void AddTrigger(GameObject gmj)
    {
        triggers.Add(gmj);
    }
    public void RemoveTrigger(GameObject gmj)
    {
        triggers.Remove(gmj);
    }
	public void Reset()
    {
        for (int g = 0; g < details.Count; g++)
        {
            References.instance.DestroyGameObject(details[g]);
        }
        details = new List<GameObject>();

        for (int g = 0; g < triggers.Count; g++)
        {
            References.instance.DestroyGameObject(triggers[g]);
        }
        triggers = new List<GameObject>();
    }

    internal void Init()
    {
        details = new List<GameObject>();
        triggers = new List<GameObject>();
    }
}
