using UnityEngine;
using System.Collections;

public class GamepadDirectionInputListener : IDirectionInputListener
{
	private float _direction;
	private float _threshold = 0.2f;

	public void Initialize()
	{
		_direction = 0f;
	}
	
	public void Update()
	{
		_direction = Input.GetAxis("_Joystick_Horizontal");
	}

	public float GetDirection()
	{
		return (Mathf.Abs(_direction) > _threshold) ? _direction : 0f;
	}
}
