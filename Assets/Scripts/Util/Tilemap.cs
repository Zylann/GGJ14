using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An orthogonal tilemap composed of quads, grouped into meshes.
/// Due to mesh size limitation and for culling optimizations, the map is internally sliced into chunks.
/// Empty zones don't generate geometry.
/// Static colliders can be generated too only on relevant faces. Only square primitive is supported.
/// Supports Tiled or plain arrays.
/// </summary>
public class Tilemap : MonoBehaviour
{
	public int chunkSize = 8;
	public Material atlas;
	[HideInInspector] public int[] collisionMapping;

	[HideInInspector][SerializeField] private GameObject[]  _chunks;
	[HideInInspector][SerializeField] private GameObject    _collider;
	[HideInInspector][SerializeField] private int           _chunksX;
	[HideInInspector][SerializeField] private int           _chunksY;
	[HideInInspector][SerializeField] private int           _width;
	[HideInInspector][SerializeField] private int           _height;
	
	// These are stored here for convenience. They are updated when the map is built,
	// and can be reused to modify specific tiles before calling BuildTiles for instance.
	// Grid are indexed by [x+y*width], in an Y-up semantic.
	[HideInInspector][SerializeField] public int[]          tileGrid;
	[HideInInspector][SerializeField] public int            tileFirstgid;
	[HideInInspector][SerializeField] public int[]          collisionGrid;
	[HideInInspector][SerializeField] public int            collisionFirstgid;
	
	#region "Tiled"
	
	// Tiled settings
	private const string BACKGROUND_LAYER = "background";
	private const string COLLISION_LAYER = "background"; //"collision";
	private const string COLLISION_TILESET = "background"; //"collision";
	
	public int width               { get { return _width; } }
	public int height              { get { return _height; } }

	public void SetLayer(int layer)
	{
		for(int i = 0; i < _chunks.Length; ++i)
		{
			if(_chunks[i] != null)
			{
				_chunks[i].layer = layer;
			}
		}
	}
	
	public void Reset()
	{
		if(_chunks != null)
		{
			Debug.Log("Delete " + _chunks.Length + " chunks");
			foreach(GameObject chunk in _chunks)
			{
				Destroy(chunk);
			}
			_chunks = null;
		}
		
		if(_collider != null)
		{
			Debug.Log("Delete tilemap collider");
			Destroy(_collider);
			_collider = null;
		}
		
		_width = 0;
		_height = 0;
		_chunksX = 0;
		_chunksY = 0;
		
		tileGrid = null;
		collisionGrid = null;
	}
	
	public void Build(TiledMap tiledMap, string tilesetName, bool collisions=true)
	{
		_width = tiledMap.width;
		_height = tiledMap.height;
		
		BuildTiles(tiledMap, tilesetName);

		if(collisions)
		{
			// Create colliders
			BuildMeshCollider(tiledMap);
		}
	}
	
	public static void ConvertGridToYUp(int[] grid, int width, int height)
	{
		int src, dst, tmp;
		
		for(int y = 0; y < height/2; ++y)
		{
			for(int x = 0; x < width; ++x)
			{
				src = x + (height-y-1) * width; // Y down
				dst = x + y * width; // Y up
				
				tmp = grid[src];
				grid[src] = grid[dst];
				grid[dst] = tmp;
			}
		}
	}
	
	/// <summary>
	/// Generates a single mesh collider from Tiled data.
	/// Only relevant faces are computed.
	/// Impprovement notes :
	/// - Meshes could be separated if the max vertice count is reached (65536)
	/// - Could use triangle strips instead of individual triangles
	/// - Have more shapes available (only boxes are supported)
	/// </summary>
	/// <param name='tiledMap'>Tiled map.</param>
	public void BuildMeshCollider(TiledMap tiledMap)
	{
		// Index Tiled data
		TiledMap.Layer layer = tiledMap.layers [COLLISION_LAYER];
		TiledMap.Tileset tileset = tiledMap.tilesets [COLLISION_TILESET];
		int[] grid = new int[layer.grid.Length];
		
		// Reverse grid Y-axis
		System.Array.Copy(layer.grid, grid, grid.Length);
		ConvertGridToYUp(grid, layer.width, layer.height);
		
		// Build collider
		BuildMeshCollider(grid, layer.width, layer.height, tileset.firstgid);
	}
	
	public void BuildTiles(TiledMap tiledMap, string tilesetName)
	{
		// Index Tiled data
		TiledMap.Layer layer = tiledMap.layers[BACKGROUND_LAYER];
		TiledMap.Tileset tileset = tiledMap.tilesets[tilesetName];
		
		// Reverse grid Y-axis
		int[] grid = new int[layer.grid.Length];
		System.Array.Copy(layer.grid, grid, grid.Length);
		ConvertGridToYUp(grid, layer.width, layer.height);
		
		// Build tiles
		BuildTiles(grid, layer.width, layer.height, tileset.firstgid, tileset.tileWidth, tileset.tileHeight);
	}
	
	#endregion
	
	public void BuildMeshCollider(int[] grid, int width, int height, int firstgid=1)
	{
		collisionGrid = grid;
		collisionFirstgid = firstgid;

		List<int> positions = new List<int>();
		List<int> directions = new List<int>();
		
		// Fetch grid to find faces
		
		for(int y = 0; y < height; ++y)
		{
			for(int x = 0; x < width; ++x)
			{
				int tc = grid[x+y*width] - firstgid;

				if(tc < 0)
				{
					continue;
				}

				if(collisionMapping != null)
				{
					tc = collisionMapping[tc];
				}

				if(tc == 1)
				{
					int tl=-1, tr=-1, tu=-1, td=-1;
					
					if(x != 0)
					{
						tl = grid[(x-1)+y*width] - firstgid;
					}
					if(x != width-1)
					{
						tr = grid[(x+1)+y*width] - firstgid;
					}
					if(y != 0)
					{
						td = grid[x+(y-1)*width] - firstgid;
					}
					if(y != height-1)
					{
						tu = grid[x+(y+1)*width] - firstgid;
					}
					
					int xy = x | (y << 16);

					if(collisionMapping != null)
					{
						if(tl >= 0)
							tl = collisionMapping[tl];
						if(tr >= 0)
							tr = collisionMapping[tr];
						if(tu >= 0)
							tu = collisionMapping[tu];
						if(td >= 0)
							td = collisionMapping[td];
					}

					if(tl != 1)
					{
						// Left face
						positions.Add(xy);
						directions.Add(Direction.LEFT);
					}
					if(tr != 1)
					{
						// Right face
						positions.Add(xy);
						directions.Add(Direction.RIGHT);
					}
					if(tu != 1)
					{
						// Up face
						positions.Add(xy);
						directions.Add(Direction.UP);
					}
					if(td != 1)
					{
						// Down face
						positions.Add(xy);
						directions.Add(Direction.DOWN);
					}
				}
			}
		}
		
		// Build the mesh
		
		int quadCount = positions.Count;
		
		Vector3[] vertices = new Vector3[4*quadCount];
		Vector3[] normals = new Vector3[4*quadCount];
		int[] triangles = new int[6*quadCount];
		
		Vector3 vleft = new Vector3(-1, 0, 0);
		Vector3 vright = new Vector3(1, 0, 0);
		Vector3 vup = new Vector3(0, 1, 0);
		Vector3 vdown = new Vector3(0, -1, 0);
				
		for(int j = 0; j < quadCount; ++j)
		{
			int d = directions[j];
			int x = positions[j] & 0x0000ffff;
			int y = (positions[j] >> 16) & 0x0000ffff;
			
			int vi = 4*j;
			
			switch(d)
			{
			case Direction.LEFT:
				
				vertices[vi  ] = new Vector3(x, y, 0);
				vertices[vi+1] = new Vector3(x, y, -1);
				vertices[vi+2] = new Vector3(x, y+1, 0);
				vertices[vi+3] = new Vector3(x, y+1, -1);
				
				normals[vi  ] = vleft;
				normals[vi+1] = vleft;
				normals[vi+2] = vleft;
				normals[vi+3] = vleft;
				
				break;
				
			case Direction.RIGHT:
				
				vertices[vi  ] = new Vector3(x+1, y+1, 0);
				vertices[vi+1] = new Vector3(x+1, y+1, -1);
				vertices[vi+2] = new Vector3(x+1, y, 0);
				vertices[vi+3] = new Vector3(x+1, y, -1);
				
				normals[vi  ] = vright;
				normals[vi+1] = vright;
				normals[vi+2] = vright;
				normals[vi+3] = vright;

				break;
				
			case Direction.DOWN:
				
				vertices[vi  ] = new Vector3(x+1, y, 0);
				vertices[vi+1] = new Vector3(x+1, y, -1);
				vertices[vi+2] = new Vector3(x, y, 0);
				vertices[vi+3] = new Vector3(x, y, -1);
				
				normals[vi  ] = vdown;
				normals[vi+1] = vdown;
				normals[vi+2] = vdown;
				normals[vi+3] = vdown;
				
				break;
			
			case Direction.UP:
				
				vertices[vi  ] = new Vector3(x, y+1, 0);
				vertices[vi+1] = new Vector3(x, y+1, -1);
				vertices[vi+2] = new Vector3(x+1, y+1, 0);
				vertices[vi+3] = new Vector3(x+1, y+1, -1);
				
				normals[vi  ] = vup;
				normals[vi+1] = vup;
				normals[vi+2] = vup;
				normals[vi+3] = vup;

				break;
			
			default:
				break;
			}
						
			int ti = 6*j;
			
			triangles[ti  ] = vi+2;
			triangles[ti+1] = vi+1;
			triangles[ti+2] = vi;
			triangles[ti+3] = vi+1;
			triangles[ti+4] = vi+2;
			triangles[ti+5] = vi+3;
		}
		
		Mesh mesh = new Mesh();
		
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.triangles = triangles;
		
		// Finally, create the collider
		
		GameObject obj = new GameObject("mapcollider (monolithic)");
		obj.transform.parent = transform;
		
		MeshCollider mc = obj.AddComponent<MeshCollider>();
		mc.sharedMesh = mesh;

		RigidbodyConstraints rbConstraints =
			      RigidbodyConstraints.FreezePositionZ
				| RigidbodyConstraints.FreezeRotationX
				| RigidbodyConstraints.FreezeRotationY
				| RigidbodyConstraints.FreezeRotationZ;
		
		Rigidbody rb = obj.AddComponent<Rigidbody>();
		rb.constraints = rbConstraints;
		rb.isKinematic = true;
		rb.useGravity = false;
		
		_collider = obj;
	}
	
	public void BuildTiles(int[] grid, int width, int height, int firstgid=1, int tileWidthPx=32, int tileHeightPx=32)
	{
		tileGrid = grid;
		tileFirstgid = firstgid;
		
		int nbChunks = 0;
		float profileTime = Time.realtimeSinceStartup;
		
		_width = width;
		_height = height;
			
		_chunksX = _width / chunkSize + 1;
		_chunksY = _height / chunkSize + 1;
		_chunks = new GameObject[_chunksX * _chunksY];
		
		Texture texture = atlas.mainTexture;
		
		int ntilesX = texture.width / tileWidthPx;
		int ntilesY = texture.height / tileHeightPx;
		float tsx = 1f / (float)ntilesX;
		float tsy = 1f / (float)ntilesY;
		
		Color white = new Color(1f,1f,1f,1f);
		
		// For each chunk position
		for(int cy = 0; cy < _chunksY; ++cy)
		{
			for(int cx = 0; cx < _chunksX; ++cx)
			{
				// Fetch non-empty tiles
				
				int minX = cx * chunkSize;
				int minY = cy * chunkSize;
				int maxX = (cx + 1) * chunkSize;
				int maxY = (cy + 1) * chunkSize;
				
				List<int> tilePositions = new List<int>();
				
				for(int y = minY; y < maxY; ++y)
				{
					for(int x = minX; x < maxX; ++x)
					{
						if(x >= 0 && x < _width && y >= 0 && y < _height)
						{
							int t = grid[x + y * width] - firstgid;
							
							if(t >= 0)
							{
								tilePositions.Add(x | (y << 16));
							}
						}
					}
				}
				
				// If there is something in this chunk
				if(tilePositions.Count > 0)
				{
					// Build mesh
					
					int quadCount = tilePositions.Count;
					
					Vector3[] vertices = new Vector3[4*quadCount];
					Vector3[] normals = new Vector3[4*quadCount];
					Vector2[] uv = new Vector2[4*quadCount];
					Color[] colors = new Color[4*quadCount];
					int[] triangles = new int[6*quadCount];
					
					for(int i = 0; i < quadCount; ++i)
					{
						int xy = tilePositions[i];
						int x = xy & 0x0000ffff;
						int y = (xy >> 16) & 0x0000ffff;
						
						int vi = 4*i;
	
						vertices[vi  ] = new Vector3(x-minX,   y-minY, 0);
						vertices[vi+1] = new Vector3(x-minX+1, y-minY, 0);
						vertices[vi+2] = new Vector3(x-minX,   y-minY+1, 0);
						vertices[vi+3] = new Vector3(x-minX+1, y-minY+1, 0);
						
						colors[vi  ] = white;
						colors[vi+1] = white;
						colors[vi+2] = white;
						colors[vi+3] = white;
															
						normals[vi  ] = new Vector3(0, 0, -1);
						normals[vi+1] = new Vector3(0, 0, -1);
						normals[vi+2] = new Vector3(0, 0, -1);
						normals[vi+3] = new Vector3(0, 0, -1);
						
						int ti = 6*i;
						
						triangles[ti  ] = vi+2;
						triangles[ti+1] = vi+1;
						triangles[ti+2] = vi;
						triangles[ti+3] = vi+1;
						triangles[ti+4] = vi+2;
						triangles[ti+5] = vi+3;
	
						// Calculate UVs
											
						int t = grid[x + y * width] - firstgid;
											
						float tx = (float)(t%ntilesX) * tsx;
						float ty = (float)(t/ntilesX) * tsy;
						
						ty = 1f - ty;
											
						uv[vi  ] = new Vector2(tx, ty-tsy);
						uv[vi+1] = new Vector2(tx+tsx, ty-tsy);
						uv[vi+2] = new Vector2(tx, ty);
						uv[vi+3] = new Vector2(tx+tsx, ty);
					}
					
					Mesh mesh = new Mesh();
					mesh.vertices = vertices;
					mesh.normals = normals;
					mesh.colors = colors;
					mesh.uv = uv;
					mesh.triangles = triangles;
	
					// Create chunk and assign the mesh
					
					GameObject chunkObj = new GameObject("mapchunk ("+cx+", "+cy +")");
					chunkObj.transform.parent = transform;
					chunkObj.transform.localPosition = new Vector3(cx*chunkSize, cy*chunkSize, 0);
					chunkObj.transform.localScale = new Vector3(1f, 1f, 1f);
					
					MeshFilter mf = chunkObj.AddComponent<MeshFilter>();
					mf.sharedMesh = mesh;
					
					MeshRenderer mr = chunkObj.AddComponent<MeshRenderer>();
					mr.sharedMaterial = atlas;
					
					int ci = cy*_chunksX + cx;
					_chunks[ci] = chunkObj;
					
					++nbChunks;
				}
			}
		}
		
		profileTime = Time.realtimeSinceStartup - profileTime;
		Debug.Log("Took " + profileTime + "s to create " + nbChunks + " chunks");
	}

}




