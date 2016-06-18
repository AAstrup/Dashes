using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CollisionSystem {

    bool initialised;
    float wallVerticalPos;
    float wallHorizontalPos;

    RoomScript currentRoom;

    public void Init()
    {
        
    }
    public void UpdateRoom(RoomScript newRoom)
    {
        currentRoom = newRoom;
        wallVerticalPos = currentRoom.GetRoomHeight() / 2 - currentRoom.wallHeight;
        wallHorizontalPos = currentRoom.GetRoomWidth() / 2 - currentRoom.wallWidth;
    }

    public Collision CollidesWithWall(Position unit) {
        var col = new Collision(currentRoom.wallWidth, unit.Pos.x, unit.Pos.y);
        //Horizontal collision
        if (unit.Pos.x > (wallHorizontalPos - unit.radius) + currentRoom.GetWorldPos().x)
            col.SetHorizontalPos(wallHorizontalPos - unit.radius + currentRoom.GetWorldPos().x);
        else if (unit.Pos.x < (-1 * (wallHorizontalPos - unit.radius) + currentRoom.GetWorldPos().x))
            col.SetHorizontalPos(-wallHorizontalPos + unit.radius + currentRoom.GetWorldPos().x);
        //Vertical collision
        if (unit.Pos.y > (wallVerticalPos - unit.radius) + currentRoom.GetWorldPos().y)
            col.SetVericalPos(wallVerticalPos - unit.radius + currentRoom.GetWorldPos().y);
        else if (unit.Pos.y < (-1 * (wallVerticalPos - unit.radius)) + currentRoom.GetWorldPos().y)
            col.SetVericalPos(-wallVerticalPos + unit.radius + currentRoom.GetWorldPos().y);
        return col;
    }

    public Collision GetWallCollidingWith(Position unit)
    {
        var col = new Collision(currentRoom.wallWidth, unit.Pos.x, unit.Pos.y);
        //Horizontal collision
        if (unit.Pos.x > (wallHorizontalPos - unit.radius) + currentRoom.GetWorldPos().x)
            col.SetHorizontalPos(wallHorizontalPos - unit.radius + currentRoom.GetWorldPos().x);
        else if (unit.Pos.x < (-1 * (wallHorizontalPos - unit.radius) + currentRoom.GetWorldPos().x))
            col.SetHorizontalPos(-wallHorizontalPos + unit.radius + currentRoom.GetWorldPos().x);
        //Vertical collision
        if (unit.Pos.y > (wallVerticalPos - unit.radius) + currentRoom.GetWorldPos().y)
            col.SetVericalPos(wallVerticalPos - unit.radius + currentRoom.GetWorldPos().y);
        else if (unit.Pos.y < (-1 * (wallVerticalPos - unit.radius)) + currentRoom.GetWorldPos().y)
            col.SetVericalPos(-wallVerticalPos + unit.radius + currentRoom.GetWorldPos().y);
        return col;
    }

    public List<IUnit> CollidesWithUnit(IUnit unit,Vector2 jump)
    {
        throw new NotImplementedException("Collision with units not yet supported!");
    }

}
