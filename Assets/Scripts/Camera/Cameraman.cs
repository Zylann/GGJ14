using UnityEngine;
using System.Collections;

public class Cameraman : MonoBehaviour
{
	// Inspector-set values
	public GameObject left_block;
	public float orthographic_size;
	public float camera_smoothing_factor;

	private Camera _camera;
	private Vector3 _target_position;

	public void Awake()
	{
		_camera = GetComponent<Camera>();

		_camera.orthographic = true;
		_camera.orthographicSize = orthographic_size;

		left_block.transform.position = transform.position - Vector3.right * orthographic_size * _camera.aspect;
	}

	public void Update()
	{
		float x_target_position = Game.Inst.m_object_player.transform.position.x + orthographic_size / 2f;

		if (x_target_position > transform.position.x)
		{
			_target_position = new Vector3 (x_target_position, transform.position.y, transform.position.z);
		}

		transform.position = Vector3.Lerp (transform.position, _target_position, camera_smoothing_factor);

	
	}
}
