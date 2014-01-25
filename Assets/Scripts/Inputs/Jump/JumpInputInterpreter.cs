using UnityEngine;
using System.Collections.Generic;

public class JumpInputInterpreter
{	
	private List<IJumpInputListener> _lst_jump_input_listeners;
	private float _jump_input_buffer = 0.1f;
	private Timer _timer_jump_input_buffer;

	public JumpInputInterpreter()
	{
		_timer_jump_input_buffer = Timer.CreateTimer(_jump_input_buffer, false);
		_timer_jump_input_buffer.SetToEnd();

		_lst_jump_input_listeners = new List<IJumpInputListener>();
		_lst_jump_input_listeners.Add(new GamepadJumpInputListener());
		_lst_jump_input_listeners.Add(new KeyboardJumpInputListener());

		foreach (IJumpInputListener jump_input_listener in _lst_jump_input_listeners)
		{
			jump_input_listener.Initialize();
		}
	}

	public void Update()
	{
		foreach (IJumpInputListener jump_input_listener in _lst_jump_input_listeners)
		{
			jump_input_listener.Update();
			
			if (jump_input_listener.GetJumpImpulse())
			{
				_timer_jump_input_buffer.Restart();
			}
		}
	}

	public void EndTolerance()
	{
		_timer_jump_input_buffer.SetToEnd();
	}

	public bool GetJumpImpulse()
	{
		if (!_timer_jump_input_buffer.HasEnded() && Game.Inst.m_collision_prober.IsGrounded())
		{
			Game.Inst.m_collision_prober.EndTolerance();
			return true;
		}
		return false;
	}

	public bool GetJumpHeld()
	{
		bool held = false;
		
		foreach (IJumpInputListener jump_input_listener in _lst_jump_input_listeners)
		{
			if (jump_input_listener.GetJumpHeld())
			{
				held = true;
				break;
			}
		}

		return held && !Game.Inst.m_collision_prober.IsGrounded();
	}
}
