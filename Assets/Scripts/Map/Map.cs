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

	void Start ()
	{
		GameObject obj = Instantiate(mapSectorPrefab) as GameObject;
		_sectors.Enqueue(obj.GetComponent<MapSector>());
	}

	void Update ()
	{
		float avatarX = Game.Inst.m_object_player.transform.position.x;
		float rightLimit = 50;

		// If the sector on the right is close enough to the avatar
		int maxX = _sectors.Peek().right;
		if(maxX - avatarX >= rightLimit)
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
			newSector.offsetX = maxX;
			_sectors.Enqueue(newSector);
		}
	}

}


