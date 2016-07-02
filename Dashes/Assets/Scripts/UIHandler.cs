using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler
{

    private Dictionary<string, RT> _rectTransforms;
    private Dictionary<string, Image> _images;
    private Dictionary<string, Text> _texts; 
    private List<ImageColorChangeAction> ImageColorChangeActions;
    private Canvas MainCanvas;

    private List<MapRoom> _mapRooms;
    private List<MapDoor> _mapDoors;

    private RectTransform _mapPlayer;

    public void Init()
    {
        References.instance.CreateGameObject(References.instance.PrefabLibrary.Prefabs["UIholder"]);

        ImageColorChangeActions = new List<ImageColorChangeAction>();
        _rectTransforms = new Dictionary<string, RT>();
        _images = new Dictionary<string, Image>();
        _texts = new Dictionary<string, Text>();

        MainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();

        foreach (var r in GameObject.Find("MainCanvas").GetComponentsInChildren<RectTransform>())
        {
            _rectTransforms.Add(r.name, new RT() { RectTransform = r, startpos = r.transform.localPosition, startsize = r.sizeDelta });
        }
        foreach (var r in GameObject.Find("MainCanvas").GetComponentsInChildren<Image>())
        {
            _images.Add(r.name, r);
        }
        foreach (var t in GameObject.Find("MainCanvas").GetComponentsInChildren<Text>())
        {
            _texts.Add(t.name, t);
        }

        DisableCombo();
        DisableBoss();

        AdjustScaleFactor();

        _mapRooms = new List<MapRoom>();
        _mapDoors = new List<MapDoor>();
    }

    public void AdjustScaleFactor()
    {
        MainCanvas.scaleFactor = Screen.width/931f;
    }

    public void UpdateBar(string name,float v,bool alphaeffect)
    {
        _rectTransforms[name].ChangeSize(v, 1);

        if (alphaeffect)
        {
            _images[name].color = new Color(1, 1, 1, 1);
            ImageColorChangeActions.Add(new ImageColorChangeAction()
            {
                ImageRef = _images[name],
                NewColor = new Color(1, 1, 1, 0.5f)
            });
        }
    }

    public void UpdateBloodDamage(float value)
    {
        _images["BloodBack"].color = new Color(1, 1, 1, (Mathf.Pow(10,1-value)/10)*0.4f);
        _images["BloodFront"].color = new Color(1, 1, 1, 0.85f);
        ImageColorChangeActions.Add(new ImageColorChangeAction()
        {
            ImageRef = _images["BloodFront"],
            NewColor = new Color(1, 1, 1, 0.0f)
        });
    }
    public void UpdateBloodHeal(float value)
    {
        _images["BloodBack"].color = new Color(1, 1, 1, (Mathf.Pow(10, 1 - value) / 10) * 0.4f);
    }

    public void UpdateCombo(int size,float timeleft)
    {
        _images["ComboBack"].fillAmount = timeleft;
        _texts["ComboText"].text = size.ToString();

        _texts["ComboText"].fontSize = Mathf.Min(30 + size*2,60);
        var s = Mathf.Min(1f, 0.6f + size*0.4f/15);
        _images["ComboFront"].gameObject.transform.localScale = new Vector3(s,s, 1);
    }

    public void EnableCombo()
    {
        _images["ComboBack"].gameObject.SetActive(true);
        _images["ComboFront"].gameObject.SetActive(true);
        _texts["ComboText"].gameObject.SetActive(true);
    }
    public void DisableCombo()
    {
        _images["ComboBack"].gameObject.SetActive(false);
        _images["ComboFront"].gameObject.SetActive(false);
        _texts["ComboText"].gameObject.SetActive(false);
    }

    public void EnableBoss()
    {
        _images["BossPanel"].gameObject.SetActive(true);
    }

    public void DisableBoss()
    {
        _images["BossPanel"].gameObject.SetActive(false);
    }

    public void UpdateLevel(int world,int level)
    {
        _texts["LevelText"].text = "Level: " + world.ToString() + "-" + level.ToString();
    }

    public void MapCreateRoom(int w,int h)
    {
        GameObject temp = References.instance.CreateGameObject(References.instance.PrefabLibrary.Prefabs["MapRoom"]);
        temp.transform.SetParent(_rectTransforms["Map"].RectTransform.transform,false);
        _mapRooms.Add(new MapRoom(temp,temp.GetComponent<RectTransform>(),w,h));
    }
    public void MapEnableRoom(int w, int h)
    {
        var temp = _mapRooms.Find(typ => typ.W == w && typ.H == h);
        if (temp != null)
        {
            temp.GB.SetActive(true);

            MapEnableDoor(w+0.5f,h);
            MapEnableDoor(w, h + 0.5f);
            MapEnableDoor(w - 0.5f, h);
            MapEnableDoor(w, h - 0.5f);
        }
    }

    public void MapCreateDoor(float w, float h)
    {
        GameObject temp = References.instance.CreateGameObject(References.instance.PrefabLibrary.Prefabs["MapDoor"]);
        temp.transform.SetParent(_rectTransforms["Map"].RectTransform.transform, false);
        _mapDoors.Add(new MapDoor(temp, temp.GetComponent<RectTransform>(), w, h));
    }

    public void MapEnableDoor(float w, float h)
    {
        var temp = _mapDoors.Find(typ => typ.W == w && typ.H == h);
        if (temp != null)
        {
            temp.GB.SetActive(true);
        }
    }

    public void MapUpdate(int playerw,int playerh)
    {
        _mapRooms.ForEach(typ => typ.Update(playerw,playerh));
        _mapDoors.ForEach(typ => typ.Update(playerw,playerh));
    }

    private class MapRoom
    {
        public GameObject GB;
        public RectTransform RT;
        public int W;
        public int H;

        public MapRoom(GameObject gb,RectTransform rt,int w,int h)
        {
            GB = gb;
            RT = rt;
            W = w;
            H = h;
        }

        public void Update(int offw,int offh)
        {
            RT.anchoredPosition = new Vector2((W-offw) * 20,(H-offh) * 11);
        }

        public void Remove()
        {
            References.instance.DestroyGameObject(GB);
            References.instance.UIHandler._mapRooms.Remove(this);
        }
    }

    private class MapDoor
    {
        public GameObject GB;
        public RectTransform RT;
        public float W;
        public float H;

        public MapDoor(GameObject gb, RectTransform rt, float w, float h)
        {
            GB = gb;
            RT = rt;
            W = w;
            H = h;
        }

        public void Update(int offw,int offh)
        {
            RT.anchoredPosition = new Vector2((W-offw) * 20,(H-offh) * 11);
        }

        public void Remove()
        {
            References.instance.DestroyGameObject(GB);
            References.instance.UIHandler._mapDoors.Remove(this);
        }
    }

    public void Update()
    {
        ImageColorChangeActions.ForEach(typ =>
        {
            typ.ImageRef.color += (typ.NewColor - typ.ImageRef.color)*Time.deltaTime*4;
            if (Mathf.Abs(typ.ImageRef.color.a - typ.NewColor.a) < 0.05f)
            {
                typ.ImageRef.color = typ.NewColor;
                ImageColorChangeActions.Remove(typ);
            }
        });
    }

    private class ImageColorChangeAction
    {
        public Image ImageRef;
        public Color NewColor;
        public float Speed;
    }
    private class RT
    {
        public RectTransform RectTransform;
        public Vector2 startpos;
        public Vector2 startsize;

        public void ChangeSize(float w,float h)
        {
            RectTransform.sizeDelta = new Vector2(startsize.x * w, startsize.y * h);
            RectTransform.localPosition = startpos - (startsize - RectTransform.sizeDelta)/2;
        }
    }

    public void DebugLog(string message)
    {
        _texts["DebugLog"].text += message + System.Environment.NewLine;
    }

    public void DebugLogClear()
    {
        _texts["DebugLog"].text = "";
    }

    public void MapCreate(RoomScript[,] rooms,int width,int height)
    {
        while(_mapRooms.Count > 0)
        {
            _mapRooms[0].Remove();
        }
        while (_mapDoors.Count > 0)
        {
            _mapDoors[0].Remove();
        } 
        _mapRooms = new List<MapRoom>();
        _mapDoors = new List<MapDoor>();
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if (rooms[w, h] != null)
                {
                    References.instance.UIHandler.MapCreateRoom(w, h);
                    if (rooms[w, h].doors.Contains(1))
                    {
                        References.instance.UIHandler.MapCreateDoor(w,h+0.5f);
                    }
                    if (rooms[w, h].doors.Contains(2))
                    {
                        References.instance.UIHandler.MapCreateDoor(w + 0.5f, h);
                    }
                }
            }
        }
        var temp = References.instance.CreateGameObject(References.instance.PrefabLibrary.Prefabs["MapPlayer"]);
        temp.transform.SetParent(_rectTransforms["Map"].RectTransform.transform, false);
        _mapPlayer = temp.GetComponent<RectTransform>();
        _mapPlayer.anchoredPosition = Vector2.zero;

        if (References.instance.MapUnknown)
        {
            _mapRooms.ForEach(typ => typ.GB.SetActive(false));
            _mapDoors.ForEach(typ => typ.GB.SetActive(false));
        }
    }

}
