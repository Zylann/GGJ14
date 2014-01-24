using UnityEngine;
using System.Collections;

public class DirectionInputInterpreter
{
	private enum DIRECTION_CONTROL_SCHEME { GAMEPAD, KEYBOARD};
	private DIRECTION_CONTROL_SCHEME _direction_control_scheme = DIRECTION_CONTROL_SCHEME.KEYBOARD;
	private IDirectionInputListener _direction_input_listener;

	public DirectionInputInterpreter()
	{
		switch (_direction_control_scheme)
		{
		case DIRECTION_CONTROL_SCHEME.GAMEPAD:
			_direction_input_listener = new GamepadDirectionInputListener();
			break;
		case DIRECTION_CONTROL_SCHEME.KEYBOARD:
			_direction_input_listener = new KeyboardDirectionInputListener();
			break;
		}

		_direction_input_listener.Initialize ();
	}

	public void Update()
	{
		_direction_input_listener.Update ();
	}

	/// <summary>
	/// Returns the player horizontal direction
	/// </summary>
	/// <returns>The player horizontal direction</returns>
	public float GetDirection()
	{
		return _direction_input_listener.GetDirection ();
	}
}
