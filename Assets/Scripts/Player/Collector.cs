using UnityEngine;
using System.Collections;

public class Collector : MonoBehaviour
{
	// Inspector-set values
	public float invulnerability_time = 1f;

	private Timer _timer_invulnerability;

	public void Awake()
	{
		_timer_invulnerability = Timer.CreateTimer(invulnerability_time, false);
		_timer_invulnerability.SetToEnd();
	}

	public void OnTriggerStay(Collider collider)
	{
		switch (collider.tag)
		{
		case "Collectible":
			PickCollectible(collider.gameObject);
			break;
		case "Spike":
			if (_timer_invulnerability.HasEnded())
			{
				Game.Inst.m_health.TakeDamage(1);
				_timer_invulnerability.Restart();
			}
			break;
		}
	}

	public void PickCollectible(GameObject collectible)
	{
		Game.Inst.m_scoring.PushPickCollectibleEvent();

		Game.Destroy(collectible);
	}
}
