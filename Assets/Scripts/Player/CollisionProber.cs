﻿using UnityEngine;
using System.Collections;

public class CollisionProber : MonoBehaviour
{
	// Inspector-set values
	public float grounded_time_tolerance = 0.1f;

	// Ground detection
	private Timer _timer_grounder;
	private bool _grounded = false;
	private bool _grounded_impulse = false;

	private Timer _timer_stop_ground_tolerance;
	private float _stop_tolerance_time = 0.1f;

	// Ceiling detection
	private float _ceiling_hugging_time_tolerance = 0.05f;
	private Timer _timer_ceiling_hugging;
	private bool _ceiling_hugging = false;
	private bool _ceiling_hugging_impulse = false;

	public void Start()
	{
		_timer_grounder = Timer.CreateTimer(grounded_time_tolerance, true);
		_timer_ceiling_hugging = Timer.CreateTimer (_ceiling_hugging_time_tolerance, true);
		
		_timer_stop_ground_tolerance = Timer.CreateTimer(_stop_tolerance_time, true);
	}

	public void Update()
	{
		if (!_timer_stop_ground_tolerance.HasEnded())
		{
			_timer_grounder.SetToEnd();
		}

		_grounded_impulse = false;
		if (IsGrounded() && !_grounded)
		{
			_grounded_impulse = true;
		}
		_grounded = IsGrounded();

		_ceiling_hugging_impulse = false;
		if (IsCeilingHugging () && !_ceiling_hugging)
		{
			_ceiling_hugging_impulse = true;
		}
		_ceiling_hugging = IsCeilingHugging();
	}

	public void OnCollisionStay(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			if (contact.normal.y > 0.5f)
			{
				_timer_grounder.Restart();
			}
			else if (contact.normal.y < -0.5f)
			{
				_timer_ceiling_hugging.Restart();
			}
		}
	}

	public void EndTolerance()
	{
		_timer_stop_ground_tolerance.Restart();
		_timer_grounder.SetToEnd();
	}

	public bool IsGrounded()
	{
		return !_timer_grounder.HasEnded();
	}

	public bool IsImpulseGrounded()
	{
		return _grounded_impulse;
	}

	public bool IsCeilingHugging()
	{
		return !_timer_ceiling_hugging.HasEnded();
	}
}