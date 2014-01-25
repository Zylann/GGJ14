using UnityEngine;
using System.Collections;

public class Scoring : MonoBehaviour
{
	// Inspector-set values
	public float _combo_cooldown = 4f;

	public int _current_combo { get; private set; }
	public int _current_score { get; private set; }

	private Timer _timer_combo;

	public void Awake()
	{
		_current_score = 1;
		_timer_combo = Timer.CreateTimer(_combo_cooldown, false);
	}

	public void Update()
	{
		if (_timer_combo.HasEnded())
		{
			_current_combo = 0;
		}
				
		DebugOverlay.Instance.Line("Combo", _current_combo);
		DebugOverlay.Instance.Line("Score", _current_score);
	}

	public void PushPickCollectibleEvent()
	{
		_timer_combo.Restart();

		_current_combo++;
		_current_score += _current_combo;
		
		Game.Inst.m_duckfield.OffsetScale(1f);
		Game.Inst.m_duckization.OffsetValue(0.05f);
	}
}
