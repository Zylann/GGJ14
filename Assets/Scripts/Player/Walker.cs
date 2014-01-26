using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class Walker : MonoBehaviour
{
	// Inspector-set values
	public float walk_speed = 105f;
	public float air_speed = 5f;

	public float air_hurt_speed = 5f;
	public float ground_hurt_speed = 10f;

	public float ground_drag = 15f;
	public float air_drag = 10f;
	public float run_bonus = 1.25f;
	public AnimationCurve ground_acceleration_movement_curve;
	public AnimationCurve air_acceleration_movement_curve;

	public bool running { get; private set; }

	private Movement _ground_acceleration_movement;
	private Movement _air_acceleration_movement;

	private DirectionInputInterpreter _direction_input_interpreter;
	private RunInputInterpeter _run_input_interpreter;

	public void Awake()
	{
		_direction_input_interpreter = new DirectionInputInterpreter();
		_run_input_interpreter = new RunInputInterpeter();

		_ground_acceleration_movement = new Movement(ground_acceleration_movement_curve);
		_air_acceleration_movement = new Movement(air_acceleration_movement_curve);
	}

	public void Update()
	{
		_direction_input_interpreter.Update();
		_run_input_interpreter.Update();

		_ground_acceleration_movement.Update();
		_air_acceleration_movement.Update();

		running = _run_input_interpreter.GetRunHeld();
		float current_run_factor = running ? run_bonus : 1f;
		Game.Inst.m_cameraman.SetCameraSmoothingToRun(running && IsMoving()); // Offset camera when running the the right

		if (_direction_input_interpreter.GetDirection () > 0f && transform.localScale.x < 0f) {
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		else if (_direction_input_interpreter.GetDirection () < 0f && transform.localScale.x > 0f)
		{
			transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}

		// Walking
		if (Game.Inst.m_collision_prober.IsGrounded())
		{
			rigidbody.drag = ground_drag;

			if (_direction_input_interpreter.GetDirection() == 0f)
			{
				_ground_acceleration_movement.Restart();
			}
			else
			{
				rigidbody.AddForce(
					Vector3.right * _direction_input_interpreter.GetDirection() * _ground_acceleration_movement.GetMovement() 
					* walk_speed * current_run_factor);
			}
		}
		// Jumping
		else
		{
			rigidbody.drag = air_drag;

			if (_direction_input_interpreter.GetDirection() == 0f)
			{
				_air_acceleration_movement.Restart();
			}
			else
			{
				rigidbody.AddForce (Vector3.right * _direction_input_interpreter.GetDirection() * _air_acceleration_movement.GetMovement()
				                    * air_speed * current_run_factor);
			}
		}
	}
	
	public void Hurt(Vector3 normal, float strength)
	{
		rigidbody.AddForce(normal * strength * (Game.Inst.m_collision_prober.IsGrounded() ? ground_hurt_speed : air_hurt_speed));
	}

	public bool IsMoving()
	{
		return _direction_input_interpreter.GetDirection() > 0f;
	}
}
