using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]

public class Walker : MonoBehaviour
{
	// Inspector-set values
	public DirectionInputInterpreter.DIRECTION_CONTROL_SCHEME direction_control_scheme;
	public float walk_speed;
	public AnimationCurve acceleration_movement_curve;

	private Movement _acceleration_movement;

	private DirectionInputInterpreter _direction_input_interpreter;

	public void Awake()
	{
		_direction_input_interpreter = new DirectionInputInterpreter(direction_control_scheme);
		_acceleration_movement = new Movement(acceleration_movement_curve);
	}

	public void Update()
	{
		_direction_input_interpreter.Update();
		_acceleration_movement.Update();

		if (_direction_input_interpreter.GetDirection () == 0f)
		{
			_acceleration_movement.Restart();
		}
		else
		{
			rigidbody.AddForce (Vector3.right * _direction_input_interpreter.GetDirection () * _acceleration_movement.GetMovement() * walk_speed);
		}
		DebugOverlay.Instance.Line("Direction", _acceleration_movement.GetMovement());
	}
}
