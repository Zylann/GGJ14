using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour
{
	public float seconds = 5f;

	void Start ()
	{
		Invoke("Kill", seconds);
	}

	void Kill()
	{
		Destroy(gameObject);
	}

}

