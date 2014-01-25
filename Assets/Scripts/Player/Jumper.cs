using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour
{	
	// Inspector-set values
	public JumpInputInterpreter.JUMP_CONTROL_SCHEME jump_control_scheme;
	public float jump_impulse_strength;
	public float jump_held_strength;
	
	private JumpInputInterpreter _jump_input_interpreter;
	
	public void Awake()
	{
		_jump_input_interpreter = new JumpInputInterpreter(jump_control_scheme);
	}
	
	public void Update()
	{
		_jump_input_interpreter.Update();

		if (_jump_input_interpreter.GetJumpImpulse())
		{
			rigidbody.AddForce(Vector3.up * jump_impulse_strength);
		}

		if (_jump_input_interpreter.GetJumpHeld())
		{
			rigidbody.AddForce(Vector3.up * jump_held_strength);
		}
	}
}
