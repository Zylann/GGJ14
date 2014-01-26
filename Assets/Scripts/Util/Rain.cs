using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour
{
	public GameObject prefab;
	public FloatRange rate = new FloatRange(0.1f, 0.7f);
	private float _time;
	
	void Update ()
	{
		_time -= Time.deltaTime;
		if(_time <= 0)
		{
			_time = rate.Rand();
			Vector3 pos = Camera.main.transform.position + 4f*Vector3.forward;
			pos.x += Random.Range(-5f, 5f);
			pos.y += Random.Range(10f, 15f);
			GameObject obj = Instantiate(prefab, pos, Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f))) as GameObject;
			if(Random.Range(0f, 1f) < 0.5f)
			{
				obj.GetComponent<RotateMove>().rotationSpeed *= -1f;
			}
		}
	}

}


