using UnityEngine;
using System.Collections;

public class CameraScript {

    GameObject GBref;
    Vector2 ScreenShakeAmount;
    Vector2 currentRoomPos;
    Vector3 targetpos;
    Vector3 Pos;

    GameObject RoomOuter;

    public void Init(GameObject camera)
    {
        GBref = camera;
        currentRoomPos = Vector2.zero;
        RoomOuter = References.instance.CreateGameObject(References.instance.PrefabLibrary.Prefabs["RoomBgnOuter"]);
    }

	// Use this for initialization
	public void SetPos (RoomScript room,bool instant) {
        currentRoomPos = room.GetWorldPos();
        targetpos = new Vector3(currentRoomPos.x + ScreenShakeAmount.x, currentRoomPos.y + ScreenShakeAmount.y, GBref.transform.position.z);
	    RoomOuter.transform.position = currentRoomPos;
	    if (instant)
	    {
            Pos = targetpos;
	    }
	}

    public void Update()
    {
        //Debug.Log("ScreenShake " + ScreenShakeAmount.ToString());
        AdjustScrenShake();
        //GBref.transform.position = new Vector3(currentRoomPos.x + ScreenShakeAmount.x, currentRoomPos.y + ScreenShakeAmount.y, GBref.transform.position.z);
        if (Vector3.Distance(GBref.transform.position, targetpos) < 0.1f)
        {
            Pos = targetpos;
        }
        else
        {
            Pos += (targetpos - Pos) * Time.deltaTime * 10f;
        }
        GBref.transform.position = new Vector3(Mathf.Round(Pos.x * 16f) / 16f, Mathf.Round(Pos.y * 16f) / 16f, GBref.transform.position.z);
    }

    private void AdjustScrenShake()
    {
        if (Mathf.Abs(ScreenShakeAmount.x) < 0.2f)
        {
            ScreenShakeAmount = Vector2.zero;
        }
        else
        {
            ScreenShakeAmount *= 1f - Time.deltaTime * 3f;
            ScreenShakeAmount = new Vector2(ScreenShakeAmount.x * GetRandom1(), ScreenShakeAmount.y * GetRandom1());
        }
    }

    private float GetRandom1()
    {
        int r = Mathf.FloorToInt(Random.Range(0f, 2f));
        if (r == 0)
            return 1;
        else
            return -1;
    }

    public void ScreenShake(float amount)
    {
        ScreenShakeAmount = new Vector2(amount, amount);
    }
}
