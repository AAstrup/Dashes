using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RoomChallengeHandler {
    Dictionary<RoomChallenge, RoomChallengeChallengeInfo> challengeToInfo;

    public void Init()
    {
        challengeToInfo = new Dictionary<RoomChallenge, RoomChallengeChallengeInfo>();
        challengeToInfo.Add(RoomChallenge.HighHP, new RoomChallengeChallengeInfo(2f, 1f, 1f, 1f, "Iron Flesh"));
        challengeToInfo.Add(RoomChallenge.FastMS, new RoomChallengeChallengeInfo(1f, 1.5f, 1f, 1f, "Runners"));
        challengeToInfo.Add(RoomChallenge.FastAS, new RoomChallengeChallengeInfo(1f, 1f, 1.5f, 1f, "Fast"));
        challengeToInfo.Add(RoomChallenge.HighDMG, new RoomChallengeChallengeInfo(1f, 1f, 1f, 1.5f, "Pumpers"));
    }

    public void ApplyChallenge(IUnit _unit, RoomChallenge challenge)
    {
        if (challenge == RoomChallenge.None)
            return;
        var unit = (AINavigation) _unit;
        unit.damage = challengeToInfo[challenge].dmgMultiplier * unit.damage;
        unit.cd = challengeToInfo[challenge].attackSpeedMultiplier / unit.cd;
        unit.MovementSpeedBase = challengeToInfo[challenge].moveSpeedMultiplier * unit.MovementSpeedBase;
        unit.HealthMax = challengeToInfo[challenge].hpMultiplier * unit.HealthMax;
        unit.HealthCurrent = unit.HealthMax;
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

public class RoomChallengeChallengeInfo
{
    public float hpMultiplier = 1f;
    public float moveSpeedMultiplier = 1f;
    public float attackSpeedMultiplier = 1f;
    public float dmgMultiplier = 1f;
    public string title = "";
    public RoomChallengeChallengeInfo(float hpMul,float msMul,float asMul, float dmgMul,string _title)
    {
        hpMultiplier = hpMul;
        moveSpeedMultiplier = msMul;
        attackSpeedMultiplier = asMul;
        dmgMultiplier = dmgMul;
        title = _title;
    }
}

public enum RoomChallenge { None, FastMS, HighHP, HighDMG, FastAS}