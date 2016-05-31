using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler
{

    private Dictionary<string, RT> _rectTransforms;
    private Dictionary<string, Image> _images; 
    private List<ImageColorChangeAction> ImageColorChangeActions; 
    


    public void Init()
    {
        References.instance.CreateGameObject(References.instance.PrefabLibrary.Prefabs["UIholder"]);

        ImageColorChangeActions = new List<ImageColorChangeAction>();
        _rectTransforms = new Dictionary<string, RT>();
        _images = new Dictionary<string, Image>();

        foreach (var r in GameObject.Find("MainCanvas").GetComponentsInChildren<RectTransform>())
        {
            _rectTransforms.Add(r.name, new RT() { RectTransform = r, startpos = r.transform.localPosition, startsize = r.sizeDelta });
        }
        foreach (var r in GameObject.Find("MainCanvas").GetComponentsInChildren<Image>())
        {
            _images.Add(r.name, r);
        }

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
