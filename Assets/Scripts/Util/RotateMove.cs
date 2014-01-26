using UnityEngine;
using System.Collections;

public class RotateMove : MonoBehaviour
{
	public float rotationSpeed = 90f;
	public Vector3 velocity = new Vector3(0f, -10f, 0f);

	void Update ()
	{
		transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
		transform.position = transform.position + velocity * Time.deltaTime;
	}

}


