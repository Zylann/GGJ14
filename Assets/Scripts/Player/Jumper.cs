using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour
{	
	// Inspector-set values
	public JumpInputInterpreter.JUMP_CONTROL_SCHEME jump_control_scheme;
	public float jump_impulse_strength;
	public float jump_held_strength;
	public AnimationCurve jump_held_movement_curve;

	private JumpInputInterpreter _jump_input_interpreter;
	private Movement _jump_held_movement;
	private bool _held_jump_since_impulse;
	
	public void Awake()
	{
		_jump_input_interpreter = new JumpInputInterpreter(jump_control_scheme);
		_jump_held_movement = new Movement (jump_held_movement_curve, false);
	}
	
	public void Update()
	{
		_jump_input_interpreter.Update();
		_jump_held_movement.Update();

		if (_jump_input_interpreter.GetJumpImpulse())
		{
			_held_jump_since_impulse = true;
			_jump_held_movement.Restart();
			rigidbody.AddForce(Vector3.up * jump_impulse_strength);
		}

		if (_jump_input_interpreter.GetJumpHeld() && !Game.Inst.m_collision_prober.IsCeilingHugging() && _held_jump_since_impulse)
		{
			_jump_input_interpreter.EndTolerance();
			rigidbody.AddForce(Vector3.up * jump_held_strength * _jump_held_movement.GetMovement());
		}
		else
		{
			_held_jump_since_impulse = false;
		}
		
		DebugOverlay.Instance.Line ("Held jump", _jump_input_interpreter.GetJumpHeld());
		DebugOverlay.Instance.Line ("Held available", _held_jump_since_impulse);
	}
}
