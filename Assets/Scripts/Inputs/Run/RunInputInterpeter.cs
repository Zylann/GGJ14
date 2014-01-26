using UnityEngine;
using System.Collections.Generic;

public class RunInputInterpeter
{
	private List<IRunInputListener> _lst_run_input_listeners;
	public bool enabled { private get; set; }

	public RunInputInterpeter()
	{
		enabled = true;
		_lst_run_input_listeners = new List<IRunInputListener>();
		_lst_run_input_listeners.Add(new GamepadRunInputListener());
		_lst_run_input_listeners.Add(new KeyboardRunInputListener());

		foreach (IRunInputListener run_input_listener in _lst_run_input_listeners)
		{
			run_input_listener.Initialize();
		}
	}

	public void Update()
	{
		foreach (IRunInputListener run_input_listener in _lst_run_input_listeners)
		{
			run_input_listener.Update();
		}
	}

	public bool GetRunHeld()
	{
		if (!enabled)
		{
			return false;
		}

		foreach (IRunInputListener run_input_listener in _lst_run_input_listeners)
		{
			if (run_input_listener.GetRunningHeld())
			{
				return true;
			}
		}
		return false;
	}
	
	public bool GetRunImpulse()
	{
		if (!enabled)
		{
			return false;
		}

		foreach (IRunInputListener run_input_listener in _lst_run_input_listeners)
		{
			if (run_input_listener.GetRunningImpulse())
			{
				return true;
			}
		}
		return false;
	}
}
