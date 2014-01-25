using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
	public string startPattern;
	public string[] patterns;
	public GameObject mapSectorPrefab;
	public int activeSectors = 3;
	private int _patternIndex;
	private Queue<MapSector> _sectors = new Queue<MapSector>();
	private MapSector _lastQueuedSector;

	void Start ()
	{
		GameObject obj = Instantiate(mapSectorPrefab) as GameObject;
		_lastQueuedSector = obj.GetComponent<MapSector>();
		_sectors.Enqueue(_lastQueuedSector);
	}

	void Update ()
	{
		float avatarX = Game.Inst.m_object_player.transform.position.x;
		float rightLimit = 50;

		// If the sector on the right is close enough to the avatar
		int maxX = _lastQueuedSector.right;
		if(maxX - avatarX < rightLimit)
		{
			// If the sector count reached the limit
			if(_sectors.Count >= activeSectors)
			{
				// Erase last sector
				MapSector lastSector = _sectors.Dequeue();
				Destroy(lastSector.gameObject);
			}

			// Append new sector on the right
			GameObject sectorObj = Instantiate(mapSectorPrefab) as GameObject;
			MapSector newSector = sectorObj.GetComponent<MapSector>();
			newSector.mapName = patterns[Random.Range(0, patterns.Length)]; // TODO do something better, currently for testing
			newSector.offsetX = maxX;
			_sectors.Enqueue(newSector);
			_lastQueuedSector = newSector;
		}
	}

}




