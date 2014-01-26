using UnityEngine;
using System.Collections;

public class Scoring : MonoBehaviour
{
	// Inspector-set values
	public float _combo_cooldown = 4f;

	public int _current_combo { get; private set; }
	public int _current_score { get; private set; }

	private float _collectible_feedback_time = 0.5f;
	private float _cancer_feedback_time = 1.5f;
	private Timer _timer_collectible_feedback;
	private Timer _timer_cancer_feedback;
	private Timer _timer_combo;

	public void Awake()
	{
		_timer_combo = Timer.CreateTimer(_combo_cooldown, false);
		_timer_collectible_feedback = Timer.CreateTimer (_collectible_feedback_time, false);
		_timer_collectible_feedback.SetToEnd();
		_timer_cancer_feedback = Timer.CreateTimer (_cancer_feedback_time, false);
		_timer_cancer_feedback.SetToEnd();

		_current_score = 0;
	}

	public void Update()
	{
		if (_timer_combo.HasEnded())
		{
			_current_combo = 0;
		}
	}

	public void PushPickCollectibleEvent()
	{
		_timer_combo.Restart();

		_current_combo++;
		_current_score += _current_combo;

		_timer_collectible_feedback.Restart();

		Game.Inst.m_duckfield.OffsetScale(1f);
		Fabric.EventManager.Instance.PostEvent("Player/Lolly");
	}

	public void PushPickCancerEvent()
	{
		_timer_cancer_feedback.Restart();

		Game.Inst.m_duckfield.OffsetScale(10f);
		Fabric.EventManager.Instance.PostEvent("Player/Sick");
	}

	public void PushPickSpikeEvent()
	{
		_timer_combo.SetToEnd();
		
		_current_combo = 0;

		Game.Inst.m_duckfield.OffsetScale(-10f);
	}

	public bool HasCollectibleFeedback()
	{
		return !_timer_collectible_feedback.HasEnded();
	}
	
	public bool HasCancerFeedback()
	{
		return !_timer_cancer_feedback.HasEnded();
	}
}
