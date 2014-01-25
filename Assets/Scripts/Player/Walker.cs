using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]

public class Walker : MonoBehaviour
{
	// Inspector-set values
	public DirectionInputInterpreter.DIRECTION_CONTROL_SCHEME direction_control_scheme;
	public float walk_speed;

	private DirectionInputInterpreter _direction_input_interpreter;

	public void Awake()
	{
		_direction_input_interpreter = new DirectionInputInterpreter(direction_control_scheme);
	}

	public void Update()
	{
		_direction_input_interpreter.Update();

		rigidbody.AddForce (Vector3.right * _direction_input_interpreter.GetDirection() * walk_speed);
	}
}
