using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler
{

    private Dictionary<string, RT> _rectTransforms;
    private Dictionary<string, Image> _images;
    private Dictionary<string, Text> _texts; 
    private List<ImageColorChangeAction> ImageColorChangeActions; 
    


    public void Init()
    {
        References.instance.CreateGameObject(References.instance.PrefabLibrary.Prefabs["UIholder"]);

        ImageColorChangeActions = new List<ImageColorChangeAction>();
        _rectTransforms = new Dictionary<string, RT>();
        _images = new Dictionary<string, Image>();
        _texts = new Dictionary<string, Text>();

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
    }

    public void UpdateBar(string name,float v)
    {
        _rectTransforms[name].ChangeSize(v, 1);

        if (name == "HealthBar")
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
        _images["BloodBack"].color = new Color(1, 1, 1, value * 0.25f);
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

}
