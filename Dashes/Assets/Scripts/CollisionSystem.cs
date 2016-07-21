using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class CollisionSystem {

    bool initialised;
    float wallVerticalPos;
    float wallHorizontalPos;

    RoomScript currentRoom;

	public BlockTypes[,] BlocksPlayer;
	public BlockTypes[,] BlocksEnemy;

	public enum BlockTypes
	{
		
		Wall,
		Hole
	}

	public class TempBlockClass
	{
		public int x;
		public int y;
		public bool AffectPlayer;
		public bool AffectEnemies;
		public BlockTypes Type;
	}

    public void Init()
    {
        
    }

	public void UpdateRoom(RoomScript newRoom, List<TempBlockClass> blocks)
    {
        currentRoom = newRoom;
        wallVerticalPos = currentRoom.GetRoomHeight() / 2 - currentRoom.wallHeight;
        wallHorizontalPos = currentRoom.GetRoomWidth() / 2 - currentRoom.wallWidth;

		BlocksPlayer = new BlockTypes[(int)currentRoom.GetRoomWidth(),(int)currentRoom.GetRoomHeight()];
		BlocksEnemy = new BlockTypes[(int)currentRoom.GetRoomWidth(),(int)currentRoom.GetRoomHeight()];

		Debug.Log (currentRoom.GetRoomWidth ());

		blocks.ForEach (typ => CreateBlock (typ));
    }

	public void CreateBlock(TempBlockClass Block)
	{
		//2==2? Init() : Init();
		if(Block.AffectEnemies){BlocksEnemy[Block.x,Block.y] = Block.Type;} 
		if (Block.AffectPlayer){BlocksPlayer [Block.x, Block.y] = Block.Type;}
		Debug.Log (Block.Type);
	}

    public Collision CollidesWithWall(Position unit) {
        var col = new Collision(currentRoom.wallWidth, unit.Pos.x, unit.Pos.y);

		var x = (int)unit.Pos.x;
		var y = (int)unit.Pos.y;

		Debug.Log (x);

		if (BlocksEnemy [x - 1, y] != null) {
			if (BlocksEnemy [x - 1, y] == BlockTypes.Wall) {
				col.SetHorizontalPos(wallHorizontalPos - unit.radius + currentRoom.GetWorldPos().x);
			}
		}

		/*

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

		*/

        return col;

    }

    public List<IUnit> CollidesWithUnit(IUnit unit,Vector2 jump)
    {
        throw new NotImplementedException("Collision with units not yet supported!");
    }

}
