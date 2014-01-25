using UnityEngine;
using System.Collections;

public class JumpInputInterpreter
{
	public enum JUMP_CONTROL_SCHEME { GAMEPAD, KEYBOARD };
	
	private JUMP_CONTROL_SCHEME _jump_control_scheme;
	private IJumpInputListener _jump_input_listener;
	private float _jump_input_buffer = 0.1f;
	private Timer _timer_jump_input_buffer;

	public JumpInputInterpreter(JUMP_CONTROL_SCHEME jump_control_scheme)
	{
		_jump_control_scheme = jump_control_scheme;
		_timer_jump_input_buffer = Timer.CreateTimer(_jump_input_buffer, false);
		_timer_jump_input_buffer.SetToEnd();

		switch (_jump_control_scheme)
		{
		case JUMP_CONTROL_SCHEME.GAMEPAD:
			_jump_input_listener = new GamepadJumpInputListener();
			break;
		case JUMP_CONTROL_SCHEME.KEYBOARD:
			_jump_input_listener = new KeyboardJumpInputListener();
			break;
		}

		_jump_input_listener.Initialize();
	}

	public void Update()
	{
		_jump_input_listener.Update();

		if (_jump_input_listener.GetJumpImpulse ())
		{
			_timer_jump_input_buffer.Restart();
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
		return _jump_input_listener.GetJumpHeld() && !Game.Inst.m_collision_prober.IsGrounded();
	}
}
