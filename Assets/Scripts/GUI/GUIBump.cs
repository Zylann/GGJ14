using UnityEngine;
using System.Collections;

public class GUIBump : MonoBehaviour
{
	public float scale = 1.5f;
	public float duration = 0.25f;
	private Vector3 _initialScale;
	private float _remainingTime;

	void Awake ()
	{
		_initialScale = transform.localScale;
	}

	void Bump()
	{
		_remainingTime = duration;
	}
	
	void Update ()
	{
		if(_remainingTime > 0.001f)
		{
			_remainingTime -= Time.deltaTime;
			if(_remainingTime < 0)
				_remainingTime = 0;
			float t = _remainingTime / duration;
			Vector3 s = _initialScale * (1f-t) + (_initialScale*scale)*t;
			transform.localScale = s;
		}
	}

}

