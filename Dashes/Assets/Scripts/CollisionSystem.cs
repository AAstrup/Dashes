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
		None,
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
		if(Block.AffectEnemies){BlocksEnemy[Block.x,Block.y] = Block.Type;} 
		if (Block.AffectPlayer){BlocksPlayer [Block.x, Block.y] = Block.Type;}
		var temp = References.instance.CreateGameObject (References.instance.PrefabLibrary.Prefabs ["MedicTargetBeam"]);
		temp.transform.position = new Vector3(Block.x,Block.y,0) + new Vector3 (-ArrayIndexCalc().x,
			- ArrayIndexCalc().y, 0);
		Debug.Log (Block.Type);
	}

	public Vector2 ArrayIndexCalc()
	{
		return new Vector2 (wallHorizontalPos+1.0f, wallVerticalPos+1.0f) - currentRoom.GetWorldPos ();
	}

    public Collision CollidesWithWall(Position unit) {
        var col = new Collision(currentRoom.wallWidth, unit.Pos.x, unit.Pos.y);
		/*
		var x = Mathf.RoundToInt(unit.Pos.x + ArrayIndexCalc().x);
		var y = Mathf.RoundToInt(unit.Pos.y + ArrayIndexCalc().y);

		Debug.Log ("X:" + x + " Y:" + y + "Block:" + BlocksEnemy[x,y].ToString());
*/
		/*if (BlocksEnemy [x + 1, y] == BlockTypes.Wall) {
			Debug.Log (BlocksEnemy [x + 1, y].ToString ());
			col.SetHorizontalPos(x + 1 - wallHorizontalPos + currentRoom.GetWorldPos ().x - 1);
		}
		*/	
/*		if (BlocksEnemy [x - 1, y] == BlockTypes.Wall) {
			Debug.Log (BlocksEnemy [x - 1, y].ToString ());
			col.SetHorizontalPos(x - ArrayIndexCalc().x + 0.5f);
		}
*/
		

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
