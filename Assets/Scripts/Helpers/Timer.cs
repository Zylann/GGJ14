using UnityEngine;
using System.Collections;

public class Timer
{
    private bool m_enabled;
    private float m_goal_time;
    private float m_current_time;

    public static Timer CreateTimer(float goal_time, bool enabled = true)
    {
        Timer timer = new Timer(goal_time, enabled);
        Game.Inst.m_time_helper.AddTimer(timer);

        return timer;
    }

    private Timer(float goal_time, bool enabled)
    {
        m_goal_time = goal_time;
        m_enabled = enabled;
        m_current_time = 0f;
    }

    public void Roll()
    {
        if (m_enabled)
        {
            m_current_time = Mathf.Min(m_goal_time, m_current_time + Time.deltaTime);
        }
    }

    public bool HasEnded()
    {
        return m_current_time >= m_goal_time;
    }

    public float GetProgress()
    {
        return m_current_time / m_goal_time;
    }

    public void Restart(float new_timer = -1f)
    {
        if (new_timer != -1f)
        {
            m_goal_time = new_timer;
        }

        m_enabled = true;
        m_current_time = 0f;
    }

	public void SetToEnd()
	{
		m_current_time = m_goal_time;
	}

    public void SetEnabled(bool enabled)
    {
        m_enabled = enabled;
    }

    public bool IsEnabled()
    {
        return m_enabled;
    }

    public void DisableUpdating()
    {
        Game.Inst.m_time_helper.RemoveTimer(this);
    }
}
