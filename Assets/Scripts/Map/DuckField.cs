using UnityEngine;
using System.Collections;

public class DuckField : MonoBehaviour
{
	// Inspector-set values
	public float min_duckfield_scale = 1f;
	public float base_duckfield_scale = 5f;
	public float max_duckfield_scale = 50f;
	public float scale_smoothing = 0.1f;

	private Vector3 _target_scale;

	public void Awake()
	{
		SetScale(base_duckfield_scale);
	}

	public void Update()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, _target_scale, scale_smoothing);
		DebugOverlay.Instance.Line("Local scale", transform.localScale.x);
	}

	public void SetScale(float scale)
	{
		_target_scale = new Vector3(scale, scale, scale);
	}

	public void OffsetScale(float offset)
	{
		float clamped_scale = Mathf.Clamp(_target_scale.x + offset, min_duckfield_scale, max_duckfield_scale);

		SetScale(clamped_scale);
	}
}
