using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DetailHandler {

    List<GameObject> details;

    public void AddDetail(GameObject gmj)
    {
        details.Add(gmj);
    }

	public void Reset()
    {
        for (int g = 0; g < details.Count; g++)
        {
            References.instance.DestroyGameObject(details[g]);
        }
        details = new List<GameObject>();

    }

    internal void Init()
    {
        details = new List<GameObject>();
    }
}
