using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public GameObject m_object_player;
	public GameObject m_object_helpers;
	public GameObject m_object_duckfield;
	public GameObject m_object_audio;
		
	public CollisionProber m_collision_prober;
	public Scoring m_scoring;
	public Health m_health;
	public DuckField m_duckfield;
	public DuckizationController m_duckization;

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
		m_object_duckfield = GameObject.Find("Duckfield");
		m_object_audio = GameObject.Find("Audio");

        // Finding Components
		m_collision_prober = m_object_player.GetComponent<CollisionProber>();
		m_scoring = m_object_player.GetComponent<Scoring>();
		m_health = m_object_player.GetComponent<Health>();

		m_time_helper = m_object_helpers.GetComponent<TimeHelper>();

		m_duckfield = m_object_duckfield.GetComponent<DuckField>();

		m_duckization = m_object_audio.GetComponent<DuckizationController>();
    }
}
