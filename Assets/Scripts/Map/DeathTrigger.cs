using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour
{
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Player")
		{
			Map.Reload();
			//Application.LoadLevel(Application.loadedLevel);
		}
	}
}
