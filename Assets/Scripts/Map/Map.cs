using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
	public string startMap = "start";
	public GameObject spikePrefab;
	public GameObject collectiblePrefab;
	public Tilemap normalTilemap;
	public Tilemap duckWorldTilemap;

	void Start()
	{
		LoadTilemap(startMap);
	}

	private void LoadTilemap(string name, int offsetX = 0)
	{
		// Build background

		// Load map data
		TiledMap tiledMap = new TiledMap();
		tiledMap.LoadFromJSON(startMap);

		// Build visible tilemap with collisions
		normalTilemap.collisionMapping = new int[] {
			1, // [0] block
			0, // [1] duck
			2, // [2] spike up
			3, // [3] coin
			0, // ...
			0,
			0,
			0,
			0
		};
		normalTilemap.Build(tiledMap, "background");

		// Build duckland tilemap without colliders
		duckWorldTilemap.Build(tiledMap, "background", false);
		duckWorldTilemap.SetLayer(LayerMask.NameToLayer("DuckWorld"));

		TiledMap.Layer bgLayer = tiledMap.layers["background"];

		// Build special colliders

		for(int y = 0; y < bgLayer.height; ++y)
		{
			for(int x = 0; x < bgLayer.width; ++x)
			{
				int t = bgLayer.AtYup(x,y); // Note: t starts at index 1

				Vector3 pos = new Vector3(offsetX+x+0.5f, y+0.5f);

				if(t == 3)
				{
					Instantiate(spikePrefab, pos, Quaternion.identity);
				}
			}
		}

		// Build collectibles

		TiledMap.Layer objectLayer = tiledMap.layers["objects"];
		for(int y = 0; y < objectLayer.height; ++y)
		{
			for(int x = 0; x < objectLayer.width; ++x)
			{
				int t = objectLayer.AtYup(x,y);
				if(t == 4)
				{
					Instantiate(collectiblePrefab, new Vector3(offsetX+x+0.5f, y+0.5f), Quaternion.identity);
				}
			}
		}

	}
	
	void Update()
	{
		
	}

}



