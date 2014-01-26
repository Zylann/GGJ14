//#define STREAMED

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
	public MapSectorNames[] levels;
	public GameObject mapSectorPrefab;
	public int activeSectors = 3;
	public int levelIndex = 0;
	private int _sectorIndex;

#if STREAMED
	private Queue<MapSector> _sectors = new Queue<MapSector>();
	private MapSector _lastQueuedSector;
#endif

	void Start ()
	{
#if STREAMED
		AppendSector(0);
		++_sectorIndex;
#else
		InstantiateAll();
#endif
	}

	private void InstantiateAll()
	{
		MapSectorNames data = levels[levelIndex];
		int offsetX = 0;
		for(int i = 0; i < data.sectors.Length; ++i)
		{
			GameObject sectorObj = Instantiate(mapSectorPrefab) as GameObject;
			MapSector newSector = sectorObj.GetComponent<MapSector>();
			newSector.mapName = levels[levelIndex].sectors[i];
			newSector.offsetX = offsetX;
			offsetX += newSector.right;
		}
	}

#if STREAMED
	void Update ()
	{
		float avatarX = Game.Inst.m_object_player.transform.position.x;
		float rightLimit = 50;

		// If the sector on the right is close enough to the avatar
		if(_lastQueuedSector.right - avatarX < rightLimit)
		{
			// If the sector count reached the limit
			if(_sectors.Count >= activeSectors)
			{
				// Erase last sector
				MapSector lastSector = _sectors.Dequeue();
				Destroy(lastSector.gameObject);
			}

			// if the current index has not reached the end of the sectors
			if(_sectorIndex < levels.Length)
			{
				// Append next sector on the right
				AppendSector(_sectorIndex);
				++_sectorIndex;
			}
		}
	}

	private void AppendSector(int i)
	{
		GameObject sectorObj = Instantiate(mapSectorPrefab) as GameObject;
		MapSector newSector = sectorObj.GetComponent<MapSector>();
		newSector.mapName = levels[levelIndex].sectors[i];
		newSector.offsetX = _lastQueuedSector != null ? _lastQueuedSector.right : 0;
		_sectors.Enqueue(newSector);
		_lastQueuedSector = newSector;
	}

#endif

	private void OnSceneLoad()
	{
#if STREAMED
		_lastQueuedSector = null;
		_sectors.Clear();
#endif
	}

}

[System.Serializable]
public class MapSectorNames
{
	public string[] sectors;
}



