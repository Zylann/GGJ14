using UnityEngine;
using System.Collections;

public class JumpInputInterpreter
{
	public enum JUMP_CONTROL_SCHEME { GAMEPAD, KEYBOARD };
	
	private JUMP_CONTROL_SCHEME _jump_control_scheme;
	private IJumpInputListener _jump_input_listener;

	public JumpInputInterpreter(JUMP_CONTROL_SCHEME jump_control_scheme)
	{
		_jump_control_scheme = jump_control_scheme;

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
	}

	public bool GetJumpImpulse()
	{
		if (_jump_input_listener.GetJumpImpulse () && Game.Inst.m_ground_prober.IsGrounded())
		{
			Game.Inst.m_ground_prober.EndTolerance();
			return true;
		}
		return false;
	}

	public bool GetJumpHeld()
	{
		return _jump_input_listener.GetJumpHeld() && !Game.Inst.m_ground_prober.IsGrounded();
	}
}
