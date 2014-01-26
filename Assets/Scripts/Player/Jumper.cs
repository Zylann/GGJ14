using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour
{	
	// Inspector-set values
	public float jump_impulse_strength;
	public float jump_held_strength;
	public AnimationCurve jump_impulse_movement_curve;
	public AnimationCurve jump_held_movement_curve;

	private JumpInputInterpreter _jump_input_interpreter;
	private Movement _jump_impulse_movement;
	private Movement _jump_held_movement;
	private bool _held_jump_since_impulse;
	
	public void Awake()
	{
		_jump_input_interpreter = new JumpInputInterpreter();
		_jump_impulse_movement = new Movement (jump_impulse_movement_curve, false);
		_jump_impulse_movement.Pause();
		_jump_held_movement = new Movement (jump_held_movement_curve, false);
	}
	
	public void Update()
	{
		_jump_input_interpreter.Update();
		_jump_held_movement.Update();
		_jump_impulse_movement.Update();

		if (_jump_input_interpreter.GetJumpImpulse())
		{
			_held_jump_since_impulse = true;
			_jump_impulse_movement.Restart();
			_jump_held_movement.Restart();
			Fabric.EventManager.Instance.PostEvent("Player/Jump");
		}

		rigidbody.AddForce(Vector3.up * jump_impulse_strength * _jump_impulse_movement.GetMovement());

		if (_jump_input_interpreter.GetJumpHeld() && !Game.Inst.m_collision_prober.IsCeilingHugging() && _held_jump_since_impulse)
		{
			_jump_input_interpreter.EndTolerance();
			rigidbody.AddForce(Vector3.up * jump_held_strength * _jump_held_movement.GetMovement());

		}
		else
		{
			_held_jump_since_impulse = false;
		}
	}
}
