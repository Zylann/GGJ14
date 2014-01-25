using UnityEngine;
using System.Collections;

public class Direction
{
	public const int LEFT = 0;
	public const int RIGHT = 1;
	public const int DOWN = 2;
	public const int UP = 3;
	public const int NONE = -1;
	
	private static Vector3[] vectors = new Vector3[] {
		new Vector3(-1, 0),
		new Vector3(1, 0),
		new Vector3(0, -1),
		new Vector3(0, 1)
	};
	
	public static string ToString(int d)
	{
		switch(d)
		{
		case Direction.LEFT: return "left";
		case Direction.RIGHT: return "right";
		case Direction.DOWN: return "down";
		case Direction.UP: return "up";
		default: return "none";
		}
	}
	
	public static Vector3 ToVector(int d)
	{
		return vectors[d];
	}
	
	public static int FromVector(Vector3 v)
	{
		if(Mathf.Approximately(v.x,0f) && Mathf.Approximately(v.y,0f) && Mathf.Approximately(v.z,0f))
		{
			return NONE;
		}
		else if(Mathf.Approximately(0, v.y))
		{
			if(v.x > 0)
			{
				return RIGHT;
			}
			else
			{
				return LEFT;
			}
		}
		else if(Mathf.Approximately(0, v.x))
		{
			if(v.y > 0)
			{
				return UP;
			}
			else
			{
				return DOWN;
			}
		}
		else
		{
			if(Mathf.Abs(v.x) > Mathf.Abs(v.y))
			{
				if(v.x > 0)
				{
					return RIGHT;
				}
				else
				{
					return LEFT;
				}
			}
			else
			{
				if(v.y > 0)
				{
					return UP;
				}
				else
				{
					return DOWN;
				}
			}
		}
	}
	
}


