using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SpriteLibrary {

    public Dictionary<string, Sprite> Sprites;

    public void Init()
    {
        Sprites = new Dictionary<string, Sprite>();

        foreach (Sprite pre in Resources.LoadAll("Sprites", typeof(Sprite)))
        {
            Sprites.Add(pre.name, pre);
        }

    }
}
