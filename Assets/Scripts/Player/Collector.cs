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
		case "Goal":
			PickGoal(collider.gameObject);
			break;
		case "Heart":
			PickHeart(collider.gameObject);
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

	public void PickHeart(GameObject heart)
	{
		Game.Inst.m_health.Heal(1);

		Game.Destroy(heart);
	}

	public void PickGoal(GameObject goal)
	{
		Game.Inst.m_walker.DisableInputs();
		Game.Inst.m_jumper.DisableInputs();
		Game.Inst.m_ig_menu._menu_state = IgMenu.IG_MENU_STATE.NEXT_LEVEL;
	}
}
