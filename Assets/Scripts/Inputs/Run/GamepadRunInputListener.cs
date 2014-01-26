using UnityEngine;
using System.Collections;

public class GamepadRunInputListener : IRunInputListener {
	private bool _running_impulse;
	private bool _running;

	public void Initialize()
	{
		_running_impulse = false;
		_running = false;
	}
	
	public void Update()
	{
		_running_impulse = Input.GetButtonDown("_Gamepad_Run_Button") || Input.GetButtonDown("_Gamepad_Run_Trigger");
		_running = Input.GetButton("_Gamepad_Run_Button") || (Input.GetAxis("_Gamepad_Run_Trigger") > 0f);
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
