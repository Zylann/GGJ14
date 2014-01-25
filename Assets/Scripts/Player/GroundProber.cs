using UnityEngine;
using System.Collections;

public class GroundProber : MonoBehaviour
{
	public float _grounded_time_tolerance = 0.125f;

	private Timer _grounded_timer;
	private bool _reset_timer_next_frame = false;

	public void Start()
	{
		_grounded_timer = Timer.CreateTimer (_grounded_time_tolerance, true);
	}

	public void Update()
	{
		if (_reset_timer_next_frame)
		{
			_reset_timer_next_frame = false;
			_grounded_timer.SetToEnd();
		}
	}

	public void OnCollisionStay(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			if (contact.normal.y > 0.5f)
			{
				_grounded_timer.Restart();
			}
		}
	}

	public void EndTolerance()
	{
		_reset_timer_next_frame = true;
		_grounded_timer.SetToEnd();
	}

	public bool IsGrounded()
	{
		return !_grounded_timer.HasEnded();
	}
}