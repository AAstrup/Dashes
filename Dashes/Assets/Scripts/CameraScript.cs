using UnityEngine;
using System.Collections;

public class CameraScript {

    GameObject GBref;
    Vector2 ScreenShakeAmount;
    Vector2 currentRoomPos;

    public void Init(GameObject camera)
    {
        GBref = camera;
        currentRoomPos = Vector2.zero;
    }

	// Use this for initialization
	public void SetPos (RoomScript room) {
        currentRoomPos = room.GetWorldPos();
        GBref.transform.position = new Vector3(currentRoomPos.x + ScreenShakeAmount.x, currentRoomPos.y + ScreenShakeAmount.y, GBref.transform.position.z);
    }

    public void Update()
    {
        //Debug.Log("ScreenShake " + ScreenShakeAmount.ToString());
        AdjustScrenShake();
        GBref.transform.position = new Vector3(currentRoomPos.x + ScreenShakeAmount.x, currentRoomPos.y + ScreenShakeAmount.y, GBref.transform.position.z);
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

    public void ScreenShake()
    {
        var amount = 0.25f;
        ScreenShakeAmount = new Vector2(amount, amount);
    }
}
