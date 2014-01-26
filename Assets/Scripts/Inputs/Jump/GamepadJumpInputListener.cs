using UnityEngine;
using System.Collections;

public class GamepadJumpInputListener : IJumpInputListener
{
	private bool _jump_impulse;
	private bool _jump_held;

	public void Initialize()
	{
		_jump_impulse = false;
		_jump_held = false;
	}
	
	public void Update()
	{
		_jump_impulse = Input.GetButtonDown("_Gamepad_Jump_AB") || Input.GetButtonDown("_Gamepad_Jump_XY");
		_jump_held = Input.GetButton("_Gamepad_Jump_AB") || Input.GetButton("_Gamepad_Jump_XY");
	}

	public bool GetJumpImpulse()
	{
		return _jump_impulse;
	}

	public bool GetJumpHeld()
	{
		return _jump_held;
	}
}
