using UnityEngine;
using System.Collections;

[System.Serializable]
public class Editor_RoomLayoutInfo
{
    [SerializeField]
    public RoomLayout.RoomLayoutOrientation orientation = RoomLayout.RoomLayoutOrientation.Vertical;

    [SerializeField]
    public RoomScript.roomType roomType = RoomScript.roomType.E;

    public Editor_RoomLayoutInfo()
    {

    }
}
