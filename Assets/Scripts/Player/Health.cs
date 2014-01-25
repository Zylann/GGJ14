using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	// Inspector-set values
	public int max_health = 3;

	private int _current_health;

	public void Awake()
	{
		_current_health = max_health;
	}

	public void TakeDamage(int damage)
	{
		_current_health = Mathf.Max(0, _current_health - damage);

		if (_current_health == 0)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
