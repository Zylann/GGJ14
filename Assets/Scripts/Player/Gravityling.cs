using UnityEngine;
using System.Collections;

public class Gravityling : MonoBehaviour
{
	// Inspector-set values
	public AnimationCurve _fall_movement_curve;
	public float _fall_strength;

	private Movement _fall_movement;

	public void Awake()
	{
		_fall_movement = new Movement(_fall_movement_curve, false);
	}

	public void Update()
	{
		_fall_movement.Update();

		if (Game.Inst.m_collision_prober.IsGrounded())
		{
			_fall_movement.Restart(true);
		}

		// Gravity is applied (at x = 0 on the curve) even when on the ground, so that we can detect
		// rigidbody collisions with ground
		rigidbody.AddForce (-1f * Vector3.up * _fall_strength * _fall_movement.GetMovement());
	}
}
