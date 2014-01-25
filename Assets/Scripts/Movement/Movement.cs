using UnityEngine;
using System.Collections;

public abstract class Movement
{
    protected Timer _timer;
	protected bool _enabled;

    public void Pause()
    {
		_enabled = false;
    }

    public virtual void Restart()
    {
		_enabled = true;
        _timer.Restart();
    }

    public abstract Vector3 GetMovement();
}
