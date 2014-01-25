using UnityEngine;
using System.Collections.Generic;

public class TimeHelper : MonoBehaviour
{
    private List<Timer> m_lst_timers = new List<Timer>();

    void Update()
    {
        foreach (Timer timer in m_lst_timers)
        {
            timer.Roll();
        }
    }

    public void AddTimer(Timer timer)
    {
        m_lst_timers.Add(timer);
    }

    public void RemoveTimer(Timer timer)
    {
        m_lst_timers.Remove(timer);
    }
}
