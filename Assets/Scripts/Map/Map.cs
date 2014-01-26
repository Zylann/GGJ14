using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
	public MapSectorNames[] levels;
	public GameObject mapSectorPrefab;
	public int levelIndex = 0;

	public void LoadLevel(int index)
	{
		Debug.Log("Loading level " + index);
		levelIndex = index;
		InstantiateAll();
	}
	
	void Start()
	{
		Debug.Log("Map.Start()");
		DontDestroyOnLoad(gameObject);
		LoadLevel(levelIndex);
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

}

[System.Serializable]
public class MapSectorNames
{
	public string[] sectors;
}



