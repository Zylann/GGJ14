using UnityEngine;
using System.Collections;

public class Cameraman : MonoBehaviour
{
	// Inspector-set values
	public GameObject left_block;
	public float orthographic_size;
	public float camera_smoothing_factor;
	public float camera_left_offset = 0.5f;
	public float up_tolerance = 1f;

	private Camera _camera;
	private Vector3 _target_position;

	public void Awake()
	{
		_camera = GetComponent<Camera>();

		_camera.orthographic = true;
		_camera.orthographicSize = orthographic_size;

		left_block.transform.position = transform.position - Vector3.right * orthographic_size * _camera.aspect;
	}

	public void LateUpdate()
	{
		float x_target_position = Game.Inst.m_object_player.transform.position.x + orthographic_size * _camera.aspect * camera_left_offset /* + orthographic_size / 2f*/;

		if (x_target_position > transform.position.x)
		{
			_target_position = new Vector3 (x_target_position, _target_position.y, transform.position.z);
		}
		
		float y_target_position = Game.Inst.m_object_player.transform.position.y;

		if (Mathf.Abs(y_target_position - transform.position.y) > up_tolerance)
		{
			_target_position = new Vector3 (_target_position.x, y_target_position, transform.position.z);
		}

		transform.position = Vector3.Lerp (transform.position, _target_position, camera_smoothing_factor);

	
	}
}
