using UnityEngine;
using System.Collections.Generic;

public class DirectionInputInterpreter
{
	private List<IDirectionInputListener> _lst_direction_input_listener;

	public DirectionInputInterpreter()
	{
		_lst_direction_input_listener = new List<IDirectionInputListener>();
		_lst_direction_input_listener.Add(new GamepadDirectionInputListener());
		_lst_direction_input_listener.Add(new KeyboardWasdDirectionInputListener());
		_lst_direction_input_listener.Add(new KeyboardArrowsDirectionInputListener());

		foreach (IDirectionInputListener direction_input_listener in _lst_direction_input_listener)
		{
			direction_input_listener.Initialize();
		}
	}

	public void Update()
	{
		foreach (IDirectionInputListener direction_input_listener in _lst_direction_input_listener)
		{
			direction_input_listener.Update();
		}
	}

	/// <summary>
	/// Returns the player horizontal direction
	/// </summary>
	/// <returns>The player horizontal direction</returns>
	public float GetDirection()
	{
		float direction = 0f;

		foreach (IDirectionInputListener direction_input_listener in _lst_direction_input_listener)
		{
			direction += direction_input_listener.GetDirection();
		}

		return Mathf.Max(-1f, Mathf.Min(1f, direction));;
	}
}
