using UnityEngine;
using System.Collections;

public class Editor_References : MonoBehaviour {
    public static Editor_References instance;//Itself

    public Editor_UIEvents UIHandler;
    public Editor_InformationHandler handler;
    public Editor_Load loader;
    public Editor_Save saver;
    public Editor_Drawer drawer;
    public PrefabLibrary prefabs;
    public Editor_Input input;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        input = new Editor_Input();
        input.Init();

        prefabs = new PrefabLibrary();
        prefabs.Init();

        handler = new Editor_InformationHandler();
        handler.Init();

        loader = new Editor_Load();
        loader.Init();

        saver = new Editor_Save();
        saver.Init();

        drawer = new Editor_Drawer();
        drawer.Init();

    }
    void Update()
    {
        input.Update();
    }

    public GameObject CreateGameObject(GameObject GB,Editor_IHasPosition entity)
    {
        return CreateGameObjectWithParameters(GB, new Vector3(entity.GetPosition().x, entity.GetPosition().y,0), Vector3.zero);
    }

    public GameObject CreateGameObjectWithParameters(GameObject GB, Vector3 pos, Vector3 rot)
    {
        return Instantiate(GB, pos, Quaternion.Euler(rot.x, rot.y, rot.z)) as GameObject;
    }
    public void DestroyGMJ(GameObject toDestroy)
    {
        Destroy(toDestroy);
    }
}
