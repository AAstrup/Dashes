using UnityEngine;
using System.Collections;

public class RoomStaticGameObjects {
    public GameObject _room;
    public SpriteRenderer[] _doors;

    public void Insert (SpriteRenderer gmj,int index)
    {
        _doors[index] = gmj;
        LockDoors();
    }
    public RoomStaticGameObjects()
    {
        _doors = new SpriteRenderer[4];
    }

    public void LockDoors()
    {
        for (int d = 0; d < _doors.Length; d++)
        {
            if (_doors[d] != null)
                _doors[d].sprite = References.instance.SpriteLibrary.Sprites["DoorClosed"];
            //_doors[d].color = new Color(0f, 0f, 0f);
        }
    }

    public void UnlockDoors()
    {
        for (int d = 0; d < _doors.Length; d++)
        {
            if (_doors[d] != null)
                //_doors[d].color = new Color(1f,1f,1f);
                _doors[d].sprite = References.instance.SpriteLibrary.Sprites["HorDoor"];
        }
    }

    public void Reset()
    {
        References.instance.DestroyGameObject(_room);
        for (int d = 0; d < _doors.Length; d++)
        {
            if(_doors[d] != null)
                References.instance.DestroyGameObject(_doors[d].gameObject);
        }
    }
}
