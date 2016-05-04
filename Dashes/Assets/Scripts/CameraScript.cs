using UnityEngine;
using System.Collections;

public class CameraScript {

    GameObject GBref;

    public void Init(GameObject camera)
    {
        GBref = camera;
    }

	// Use this for initialization
	public void SetPos (RoomScript room) {
        var StartPos = room.GetWorldPos();
        GBref.transform.position = new Vector3(StartPos.x, StartPos.y, GBref.transform.position.z);
    }
}
