using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
	public string startMap = "start";
	private Tilemap _tilemap;

	void Start()
	{
		TiledMap tiledMap = new TiledMap();
		tiledMap.LoadFromJSON(startMap);

		_tilemap = GetComponent<Tilemap>();
		_tilemap.collisionMapping = new int[] {
			1, // block
			0, // duck
			2, // spike
			0, // ...
			0,
			0,
			0,
			0,
			0
		};
		_tilemap.Build (tiledMap, "background");
	}
	
	void Update()
	{
		
	}

}



