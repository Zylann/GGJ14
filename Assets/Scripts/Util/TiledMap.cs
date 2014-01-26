using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

/// <summary>
/// Simple JSON Tiled map importer based on SimpleJSON.
/// It only transforms a file into C# objects, graphics or collisions are not part of its job.
/// Uses SimpleJSON (from the Unity3D community).
/// Note: not all properties are parsed... yet.
/// </summary>
public class TiledMap
{
	public int width;
	public int height;
	public int tileWidth;
	public int tileHeight;
	public int version;
	
	public Dictionary<string,Tileset> tilesets;
	public Dictionary<string,Layer> layers;
	
	public class Layer
	{
		public int x;
		public int y;
		public int width;
		public int height;
		public string name;
		public int[] grid;
		public Obj[] objects;
		
		public int At(int x, int y)
		{
			return grid[y*width+x];
		}
		
		public int AtYup(int x, int y)
		{
			return grid[(height-y-1)*width+x];
		}
	}
	
	public class Tileset
	{
		public string name;
		public int firstgid;
		public int tileWidth;
		public int tileHeight;
		public string texturePath;
	}
	
	public class Obj
	{
		public int gid;
		public int width;
		public int height;
		public string name;
		public string type;
		public bool visible;
		public int x;
		public int y;
	}
	
	public TiledMap()
	{
		layers = new Dictionary<string, Layer>();
		tilesets = new Dictionary<string, Tileset>();
	}
		
	public virtual bool LoadFromJSON(string name)
	{
		TextAsset jsonText = (TextAsset)Resources.Load(name, typeof(TextAsset));
		if(jsonText == null)
		{
			Debug.LogError("TextAsset not found (" + name +")");
			return false;
		}

		Debug.Log("Loading TiledMap " + name);
		
		JSONNode n = JSON.Parse(jsonText.text);
		
		// Root data
		
		version = n["version"].AsInt;
		width = n["width"].AsInt;
		height = n["height"].AsInt;
		tileWidth = n["tilewidth"].AsInt;
		tileHeight = n["tileheight"].AsInt;
		
		// Tilesets
		
		JSONArray a = n["tilesets"].AsArray;
		
		for(int i = 0; i < a.Count; ++i)
		{
			Tileset tileset = new Tileset();
			
			tileset.firstgid = a[i]["firstgid"].AsInt;
			tileset.texturePath = a[i]["image"].Value;
			tileset.name = a[i]["name"].Value;
			tileset.tileWidth = a[i]["tilewidth"].AsInt;
			tileset.tileHeight = a[i]["tileheight"].AsInt;
			
			tilesets[tileset.name] = tileset;
		}

		// Layers
		
		a = n["layers"].AsArray;
		
		for(int i = 0; i < a.Count; ++i)
		{
			Layer layer = new Layer();
			
			// Common data
			
			layer.x = a[i]["x"].AsInt;
			layer.y = a[i]["y"].AsInt;
			layer.width = a[i]["width"].AsInt;
			layer.height = a[i]["height"].AsInt;
			layer.name = a[i]["name"].Value;
			
			string layerType = a[i]["type"];
			
			if(layerType.Equals("tilelayer"))
			{
				// Tiles
				
				JSONArray data = a[i]["data"].AsArray;
				layer.grid = new int[data.Count];
	
				for(int j = 0; j < layer.grid.Length; ++j)
				{
					layer.grid[j] = data[j].AsInt;
				}
			}
			else if(layerType.Equals("objectgroup"))
			{
				// Objects
				
				JSONArray objs = a[i]["objects"].AsArray;
				layer.objects = new TiledMap.Obj[objs.Count];
				
				for(int j = 0; j < layer.objects.Length; ++j)
				{
					Obj obj = new Obj();
					
					obj.x = objs[j]["x"].AsInt;
					obj.y = objs[j]["y"].AsInt;
					obj.gid = objs[j]["gid"].AsInt;
					obj.visible = objs[j]["visible"].AsBool;
					obj.width = objs[j]["width"].AsInt;
					obj.height = objs[j]["height"].AsInt;
					
					layer.objects[j] = obj;
				}
			}
			else
			{
				Debug.LogError("TiledMap: unknown or unsupported layer type \"" + layerType + '"');
			}
			
			layers[layer.name] = layer;
		}
		
		return true;
	}

}








