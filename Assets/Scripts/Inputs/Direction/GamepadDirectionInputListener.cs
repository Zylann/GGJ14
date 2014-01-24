using UnityEngine;
using System.Collections;

public class GamepadDirectionInputListener : IDirectionInputListener
{
	private float _direction;

	public void Initialize()
	{
		_direction = 0f;
	}
	
	public void Update()
	{
		//_direction = 
	}

	public float GetDirection()
	{
		return _direction;
	}
}
