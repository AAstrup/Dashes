using UnityEngine;
using System.Collections;
using System;

public class RoomChallengeHandler {

    public void Init()
    {
    }

    public void ApplyChallenge(IUnit unit, RoomChallenge challenge)
    {

        Debug.Log("NOTHING HAPPENS WHEN APPLYING CHALLENGE!");
    }

    public RoomChallenge GenerateChallenge()
    {
        float random = UnityEngine.Random.Range(0f, 2f);
        if (random < 1f) {
            return RoomChallenge.None;
        }
        else {
            int challengesAmount = Enum.GetNames(typeof(RoomChallenge)).Length - 1;// - 1 because None should not be the one
            int indexAmount = 100 / challengesAmount;//For each index this is the amount is needed
            int index = Mathf.FloorToInt(UnityEngine.Random.Range(0f, 100f) / indexAmount);
            return (RoomChallenge) index + 1;// + 1 as we excluded None
        }
    }
}

public enum RoomChallenge { None, Fast, Healthy, Strong}