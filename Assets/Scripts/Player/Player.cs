using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private DirectionInputInterpreter _direction_input_interpreter;

	public void Start()
	{
		_direction_input_interpreter = new DirectionInputInterpreter ();
	}

	public void Update()
	{
		_direction_input_interpreter.Update ();
		Debug.Log (_direction_input_interpreter.GetDirection ());
	}
}
