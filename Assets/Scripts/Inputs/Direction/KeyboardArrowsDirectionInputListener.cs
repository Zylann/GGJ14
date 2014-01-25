using UnityEngine;
using System.Collections;

public class KeyboardArrowsDirectionInputListener : IDirectionInputListener
{
	private float _direction;
	
	public void Initialize()
	{
		_direction = 0f;
	}
	
	public void Update()
	{
		_direction = 0f;
		_direction += Input.GetButton ("_Keyboard_Arrow_Left") ? -1f : 0f;
		_direction += Input.GetButton ("_Keyboard_Arrow_Right") ? +1f : 0f;
	}
	
	public float GetDirection()
	{
		return _direction;
	}
}
