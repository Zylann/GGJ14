using UnityEngine;
using System.Collections;

public class KeyboardJumpInputListener : IJumpInputListener
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
		_jump_impulse = Input.GetButtonDown("_Keyboard_Jump");
		_jump_held = Input.GetButton("_Keyboard_Jump");
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
