using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	// Inspector-set values
	public int base_health = 3;
	public int max_health = 10;

	private int _current_health;

	public void Awake()
	{
		_current_health = base_health;
	}

	public void Update()
	{
		DebugOverlay.Instance.Line("Life", _current_health);
	}
	
	public int current
	{
		get { return _current_health; }
	}

	public void TakeDamage(int damage)
	{
		_current_health = Mathf.Max(0, _current_health - damage);

		if (_current_health == 0)
		{
			Fabric.EventManager.Instance.PostEvent("Player/Die");
			Fabric.EventManager.Instance.PostEvent("Music", Fabric.EventAction.StopSound);
			
			Game.Inst.m_walker.DisableInputs();
			Game.Inst.m_jumper.DisableInputs();

			Game.Inst.m_screen_helper.StartLogoFade();
		}
		else
		{
			Fabric.EventManager.Instance.PostEvent("Player/Hurt");
		}
	}

	public void Heal(int heal)
	{
		_current_health = Mathf.Clamp(_current_health + heal, 0, max_health);
	}
}
