using UnityEngine;
using System.Collections;

public class KeyboardRunInputListener : IRunInputListener {
	private bool _running_impulse;
	private bool _running;

	public void Initialize()
	{
		_running_impulse = false;
		_running = false;
	}
	
	public void Update()
	{
		_running_impulse = Input.GetButtonDown("_Keyboard_Run");
		_running = Input.GetButton("_Keyboard_Run");
	}

	public bool GetRunningImpulse()
	{
		return _running_impulse;
	}

	public bool GetRunningHeld()
	{
		return _running;
	}
}
