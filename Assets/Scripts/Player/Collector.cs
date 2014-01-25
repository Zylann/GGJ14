using UnityEngine;
using System.Collections;

public class Collector : MonoBehaviour
{
	public void OnTriggerStay(Collider collider)
	{
		switch (collider.tag)
		{
		case "Collectible":
			PickCollectible(collider.gameObject);
			break;
		case "Cancer":
			PickCancer(collider.gameObject);
			break;
		}
	}

	public void PickCollectible(GameObject collectible)
	{
		Game.Inst.m_scoring.PushPickCollectibleEvent();

		Game.Destroy(collectible);
	}

	public void PickCancer(GameObject cancer)
	{
		Game.Inst.m_scoring.PushPickCancerEvent();

		Game.Destroy(cancer);	
	}
}
