using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public GameObject m_object_player;
	public GameObject m_object_helpers;
		
	public CollisionProber m_collision_prober;
	public TimeHelper m_time_helper;

    private static Game instance;
    public static Game Inst
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ReferenceHelper").AddComponent<Game>();
                instance.transform.parent = GameObject.Find("Helpers").transform;
                instance.Initialize();
            }
            return instance;
        }
    }

    public void OnApplicationQuit()
    {
        instance = null;
    }

    public void DestroyInstance()
    {
        instance = null;
    }

    public void Initialize()
    {
        // Finding GameObjects
        m_object_player = GameObject.Find("Player");
		m_object_helpers = GameObject.Find ("Helpers");

        // Finding Components
		m_collision_prober = m_object_player.GetComponent<CollisionProber>();
		m_time_helper = m_object_helpers.GetComponent<TimeHelper>();
    }
}
