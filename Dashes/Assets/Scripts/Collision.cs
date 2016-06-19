using UnityEngine;
using System.Collections;

public class Collision
{
    public bool collided = false;
    Vector2 newPosition;
    Vector2 normalVector;
    public float width;
    public Collision(float _width,float _horizontalPos,float _verticalPos) {
        width = _width;
        newPosition = new Vector2(_horizontalPos,_verticalPos);
    }

    public void SetHorizontalPos(float a)
    {
        collided = true;
        newPosition = new Vector2(a, newPosition.y);
        normalVector = new Vector2(ConvertToDir(a), normalVector.y);
    }
    
    float ConvertToDir(float a)
    {
        if (a > 0)
            return 1f;
        else
            return -1f;
    }

    public void SetVericalPos(float a)
    {
        collided = true;
        newPosition = new Vector2(newPosition.x, a);
        normalVector = new Vector2(normalVector.x, ConvertToDir(a));
    }
    public Vector2 GetFinalPos() { return newPosition; }//The updated position
    public bool Collided() { return collided; }
    public Vector2 GetCollisionNormal() { return normalVector; }
}
