using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerInput
{

    public Dictionary<string,PIButton> Buttons = new Dictionary<string, PIButton>()
    {
        {"Left",new PIButton("Horizontal",1,true)},
        {"Right",new PIButton("Horizontal",-1,true)},
        {"Up",new PIButton("Vertical",1,true)},
        {"Down",new PIButton("Vertical",-1,true)},
        {"Attack",new PIButton("Fire1",1,true)},
        {"Attack2",new PIButton("Fire2",1,true)}
    }; 

    public Vector2 Dir;

    public void Update()
    {

    

        Buttons.Values.ToList().ForEach(typ =>
        {
            typ.Pressed = typ.Pressing;
            typ.Pressing = typ.Raw ? Input.GetAxisRaw(typ.AxisName) * Mathf.Abs(typ.Min) / typ.Min >= Mathf.Abs(typ.Min) : 
                Input.GetAxis(typ.AxisName) * Mathf.Abs(typ.Min) / typ.Min > Mathf.Abs(typ.Min);
        });

        Dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }

    public void Init()
    {
        /*Ikke noget endnu :3*/
    }

    public Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }


}

public class PIButton
{
    public bool Pressing;
    public bool Pressed;
    public string AxisName;
    public float Min;
    public bool Raw; 

    public PIButton(string axisName,float min,bool raw)
    {
        AxisName = axisName;
        Min = min;
        Raw = raw;
    }
}
