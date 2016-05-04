using UnityEngine;
using System.Collections;

public class Collision
{
    public bool collided = false;
    Vector2 pos;
    public float width;
    public Collision(float _width,float _horizontalPos,float _verticalPos) {
        width = _width;
        pos = new Vector2(_horizontalPos,_verticalPos);
    }

    public void SetHorizontalPos(float a)
    {
        collided = true;
        pos = new Vector2(a, pos.y);
    }

    public void SetVericalPos(float a)
    {
        collided = true;
        pos = new Vector2(pos.x, a);
    }
    public Vector2 GetFinalPos() { return pos; }
    public bool Collided() { return collided; }
}
