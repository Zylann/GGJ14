using UnityEngine;
using System.Collections;

public class Collector : MonoBehaviour
{
	public void OnTriggerEnter(Collider collider)
	{
		switch (collider.tag)
		{
		case "Collectible":
			PickCollectible(collider.gameObject);
			break;
		default:

			break;
		}
	}

	public void PickCollectible(GameObject collectible)
	{
		Game.Inst.m_scoring.PushPickCollectibleEvent();

		Game.Destroy(collectible);
	}
}
