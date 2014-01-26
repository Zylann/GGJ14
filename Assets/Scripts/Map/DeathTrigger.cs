using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour
{
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Player")
		{
			Game.Inst.m_health.TakeDamage(20);
		}
	}
}
