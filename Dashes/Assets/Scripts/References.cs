﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class References : MonoBehaviour
{

    public static References instance;//Itself

    public ProgressionHandler progressionHandler;
    public GameObject mainCamera;
    public CameraScript cameraScript;
    public MapGenerator mapGenerator;
    public CollisionSystem colSystem;
    public PrefabLibrary PrefabLibrary;//prefabs used ingame
    public SpriteLibrary SpriteLibrary;
    public UnitHandler UnitHandler;//Class responsible for actions regarding units
    public TriggerHandler triggerHandler;//Responsible for everything which triggers depending on distance
    public ParticleEffectHandler particleHandler;
    public PlayerInput PlayerInput;
    public AspectHandler AspectHandler;
    public RoomHandler RoomHandler;

    public bool MapUnknown;

    public void Reset()
    {
        if (UnitHandler != null)
            UnitHandler.Reset();
        if (DetailHandler != null)
            DetailHandler.Reset();
        if(triggerHandler != null)
            triggerHandler.Reset();
        if (mapGenerator != null)
            mapGenerator.Reset();
    }

    public void EnterRoomTrigger()
    {
        UnitHandler.Reset();
    }

    public RoomLayoutHandler RoomLayoutHandler;
    public SpawnHandler SpawnHandler;
    public UIHandler UIHandler;
    public DetailHandler DetailHandler;
    public RoomChallengeHandler RoomChallengeHandler;

    private Archer boss;

    void Awake()
    {
        instance = this;
    }
	void Start ()
	{
	    MapUnknown = true;

        PrefabLibrary = new PrefabLibrary();//Should be loaded in game start
        PrefabLibrary.Init();

        cameraScript = new CameraScript();
        cameraScript.Init(GameObject.Find("Main Camera"));
        RenderSettings.ambientLight = Color.white;
        RenderSettings.ambientIntensity = 1.5f;

        mainCamera = GameObject.Find("Main Camera");

        SpriteLibrary = new SpriteLibrary();
        SpriteLibrary.Init();
        
        colSystem = new CollisionSystem();
        colSystem.Init();

        particleHandler = new ParticleEffectHandler();
        particleHandler.Init();

        UnitHandler = new UnitHandler();
        UnitHandler.Init();

        triggerHandler = new TriggerHandler();
        triggerHandler.Init();

        PlayerInput = new PlayerInput();
        PlayerInput.Init();

        AspectHandler = new AspectHandler();
        AspectHandler.Init();

        UIHandler = new UIHandler();
	    UIHandler.Init();

        DetailHandler = new DetailHandler();
        DetailHandler.Init();

        RoomChallengeHandler = new RoomChallengeHandler();
        RoomChallengeHandler.Init();

        progressionHandler = new ProgressionHandler();
        progressionHandler.Init();
    }

    public void UpdateReferences()
    {
        RoomLayoutHandler = progressionHandler.GetRoomLayoutHandler();
        SpawnHandler = progressionHandler.GetSpawnHandler();
        mapGenerator = progressionHandler.GetMapGenerator();
        RoomHandler = progressionHandler.GetRoomHandler();
    }

    void Update () {
	
        UnitHandler.Update();
        PlayerInput.Update();
        triggerHandler.Update();
        AspectHandler.Update();
        RoomHandler.Update();
        UIHandler.Update();
        cameraScript.Update();

        //boss.Pos = UnitHandler.playerController.Pos + new Vector2(0, 10);
        //boss.Update();

        //TEST
        if (Input.GetKeyDown(KeyCode.Space))
            UIHandler.PresentNewAspect();
    }

    public GameObject CreateGameObject(GameObject GB)
    {
        return CreateGameObjectWithParameters(GB, Vector3.zero, Vector3.zero);
    }

    public GameObject CreateGameObjectWithParameters(GameObject GB,Vector3 pos,Vector3 rot)
    {
        return Instantiate(GB, pos, Quaternion.Euler(rot.x,rot.y,rot.z)) as GameObject;
    }

    public GameObject CreatePrefabWithParameters(string prefabName, Vector3 pos, Vector3 rot)
    {
        return Instantiate(PrefabLibrary.Prefabs[prefabName], pos, Quaternion.Euler(rot.x, rot.y, rot.z)) as GameObject;
    }

    public void DestroyGameObject(GameObject GB)
    {
        Destroy(GB);
    }

    public RoomStaticGameObjects CreateRoomPrefab(RoomScript room,int x,int y)
    {
        RoomStaticGameObjects gmjs = new RoomStaticGameObjects();
        var doors = room.doors;
        var fixedPos = new Vector2(Mathf.Abs(room._pos.x), Mathf.Abs(room._pos.y));
        string PrefabName = progressionHandler.GetCurrentWorld().GetRoomPrefabName();
        var prefab = Instantiate(PrefabLibrary.Prefabs[PrefabName], fixedPos + new Vector2(x*16,y*10), Quaternion.identity) as GameObject;
        gmjs._room = prefab;
        if (room.doors.Contains(0))
            MakeDoor(Instantiate(PrefabLibrary.Prefabs[PrefabName + "HorDoor"], fixedPos + new Vector2(x * 16 - 7f /*6.84*/, y * 10), Quaternion.identity) as GameObject, new Vector3(1, 1, 1), Quaternion.identity, gmjs, 0);
        if (room.doors.Contains(1))
            MakeDoor(Instantiate(PrefabLibrary.Prefabs[PrefabName + "VerDoor"], fixedPos + new Vector2(x * 16, y * 10 + 4.325f + 0.363f /*4.4f*/), Quaternion.identity) as GameObject, new Vector3(1, -1, 1), Quaternion.Euler(0, 0, -90), gmjs, 1);
        if (room.doors.Contains(2))
            MakeDoor(Instantiate(PrefabLibrary.Prefabs[PrefabName + "HorDoor"], fixedPos + new Vector2(x * 16 + 7f /*6.84*/, y * 10), Quaternion.identity) as GameObject, new Vector3(-1, 1, 1),Quaternion.identity, gmjs, 2);
        if (room.doors.Contains(3))
            MakeDoor(Instantiate(PrefabLibrary.Prefabs[PrefabName + "VerDoor"], fixedPos + new Vector2(x * 16, y * 10 - 4.325f - 0.363f /*4.4f*/), Quaternion.identity) as GameObject, new Vector3(1, 1, 1), Quaternion.Euler(0,0,90), gmjs, 3);
        return gmjs;
    }

    void MakeDoor(GameObject door, Vector3 scale,Quaternion rot, RoomStaticGameObjects gmjs,int index)
    {
        door.transform.localScale = new Vector3(scale.x*1.0f/** 1.6125f*/,scale.y*1.0f/** 1.65f*/, 1);
        door.transform.rotation = rot;
        gmjs.Insert(door.GetComponent<SpriteRenderer>(), index);
    }

    public void Reload()
    {
        SceneManager.LoadScene("test");
    }

    public void ChooseAspect(int nr)
    {
        UIHandler.ChooseAspect(nr);
    }
}
//6.312
