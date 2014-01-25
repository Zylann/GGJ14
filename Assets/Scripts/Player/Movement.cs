using UnityEngine;
using System.Collections;

public class Movement
{
	private bool _enabled;
	private AnimationCurve _curve;
	private float _time;

	public Movement(AnimationCurve curve, bool start = true)
	{
		_curve = curve;
		_enabled = start;
	}

	public void Update()
	{
		if (_enabled)
		{
			_time += Time.deltaTime;
		}
	}

	public void Pause()
    {
		_enabled = false;
    }

	public void Resume()
	{
		_enabled = true;
	}

    public virtual void Restart(bool enabled = true)
    {
		_time = 0f;
		_enabled = enabled;
    }

    public float GetMovement()
	{
		if (_enabled == false)
		{
			return 0f;
		}

		return _curve.Evaluate (_time);
	}
}
