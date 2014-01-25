using UnityEngine;
using System.Collections;

public class CopyCamera : MonoBehaviour
{
	public Camera source;
	private Camera _camera;

	void Start()
	{
		_camera = this.camera;
	}

	void Update()
	{
		_camera.orthographicSize = source.orthographicSize;
	}

}
