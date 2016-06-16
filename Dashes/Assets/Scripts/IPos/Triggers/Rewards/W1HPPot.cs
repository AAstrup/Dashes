using UnityEngine;
using System.Collections;

public class W1HPPot : Potion {

	public W1HPPot(Vector2 pos, IUnit player)
    {
        CreatePotion("Trigger_W1HPPot", 5, pos,player);
    }
}
