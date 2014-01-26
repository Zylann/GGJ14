using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
	public static int levelIndex = 0;

	public static Map Instance { get { return __instance; } }
	private static Map __instance;

	public GameObject mapSectorPrefab;
	public MapSectorNames[] levels;

	void Awake()
	{
		__instance = this;
	}

	public void LoadLevel(int index)
	{
		Debug.Log("Loading level " + index);
		levelIndex = index;
		InstantiateAll();
	}
	
	void Start()
	{
		Debug.Log("Map.Start()");
		//DontDestroyOnLoad(gameObject);
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
			newSector.Load();
			offsetX = newSector.right;
		}
	}

	public static void Reload()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public static void LoadNext()
	{
		++levelIndex;
		Application.LoadLevel(Application.loadedLevel);
	}

}

[System.Serializable]
public class MapSectorNames
{
	public string[] sectors;
}



