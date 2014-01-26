using UnityEngine;
using System.Collections;

public class GUIHealthBar : MonoBehaviour
{
	private const int MAX = 16; // To allow testing with 30000 HP

	public float spacing = 1.1f;
	private GameObject[] _hearts;
	private int _displayedHealth;

	void Start ()
	{
		int maxHealth = Mathf.Clamp(Game.Inst.m_health.max_health, 1, MAX);
		_hearts = new GameObject[maxHealth];

		GameObject prefab = transform.GetChild(0).gameObject;
		Vector3 pos = prefab.transform.localPosition;

		for(int i = 0; i < _hearts.Length; ++i)
		{
			_hearts[i] = Instantiate(prefab) as GameObject;
			_hearts[i].transform.parent = this.transform;
			_hearts[i].transform.localPosition = pos;
			pos.x -= spacing;
		}

		_displayedHealth = 0;
	}

	void Update()
	{
		int h = Game.Inst.m_health.current;

		// Does HP changed?
		if(h != _displayedHealth)
		{
			// Update hearths
			_displayedHealth = h;
			for(int i = 0; i < _hearts.Length; ++i)
			{
				if(i < h)
				{
					_hearts[i].SetActive(true);
				}
				else
				{
					_hearts[i].SetActive(false);
				}
			}
		}
	}

}





