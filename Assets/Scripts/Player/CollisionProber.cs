using UnityEngine;
using System.Collections;

public class CollisionProber : MonoBehaviour
{
	// Inspector-set values
	public float grounded_time_tolerance = 0.1f;
	public float invulnerability_time = 1f;

	// Ground detection
	private Timer _timer_grounder;
	private bool _grounded = false;
	private bool _grounded_impulse = false;

	private Timer _timer_stop_ground_tolerance;
	private float _stop_tolerance_time = 0.1f;

	// Ceiling detection
	private float _ceiling_hugging_time_tolerance = 0.05f;
	private Timer _timer_ceiling_hugging;

	// Danger detection
	private Timer _timer_invulnerability;

	public void Awake()
	{
		_timer_grounder = Timer.CreateTimer(grounded_time_tolerance, true);
		_timer_ceiling_hugging = Timer.CreateTimer (_ceiling_hugging_time_tolerance, true);
		
		_timer_stop_ground_tolerance = Timer.CreateTimer(_stop_tolerance_time, true);
		
		_timer_invulnerability = Timer.CreateTimer(invulnerability_time, false);
		_timer_invulnerability.SetToEnd();
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
	}

	public void OnCollisionStay(Collision collision)
	{
		switch (collision.gameObject.tag)
		{
		case "Spike":
			if (_timer_invulnerability.HasEnded())
			{
				Game.Inst.m_walker.Hurt(collision.contacts[0].normal, 2.5f);
				Game.Inst.m_health.TakeDamage(1);
				Game.Inst.m_collision_prober.EndTolerance();
				_timer_invulnerability.Restart();
			}
			break;
		default:

			break;
		}

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