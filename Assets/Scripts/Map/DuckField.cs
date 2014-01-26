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
	}

	public void SetScale(float scale)
	{
		_target_scale = Mathf.Max(min_duckfield_scale, scale) * Vector3.one;
	}

	public void OffsetScale(float offset)
	{
		SetScale(_target_scale.x + offset);
		Game.Inst.m_duckization.duckizationAmount = Mathf.Min(1f, _target_scale.x / max_duckfield_scale);
	}
}
