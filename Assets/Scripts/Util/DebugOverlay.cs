using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugOverlay : MonoBehaviour
{
	// If you are exporting a build, please comment out your calls to DebugOverlay in order
	// to reduce overhead. If you want them anyway, comment #if UNITY_EDITOR into 
	// this file.
//#if UNITY_EDITOR
	
	private static DebugOverlay __instance;
	
	public static DebugOverlay Instance
	{
		get
		{
			if(__instance == null)
			{
				GameObject obj = new GameObject("DebugOverlay");
				__instance = obj.AddComponent<DebugOverlay>();
				__instance.OnCreate();
			}
			return __instance;
		}
	}
	
//#endif
	
	// Used to draw quads on the GUI
	public static Texture2D            tex;
	
	public bool                        show;
	private GUIStyle                   _style;
	private Dictionary<string,string>  _texts = new Dictionary<string, string>();
	private Dictionary<string,float>   _bars = new Dictionary<string, float>();
	private DebugHistogram             _histogram;
	
	void OnCreate()
	{
		_style = new GUIStyle();
		
		tex = new Texture2D(2, 2, TextureFormat.ARGB32, false, false);
		tex.anisoLevel = 1;
		tex.filterMode = FilterMode.Point;
		tex.SetPixel(0,0,new Color(1f,1f,1f));
		tex.SetPixel(1,0,new Color(1f,1f,1f));
		tex.SetPixel(1,1,new Color(1f,1f,1f));
		tex.SetPixel(0,1,new Color(1f,1f,1f));
		tex.Apply();
	}
	
	public DebugOverlay Line<T>(string ID, T v)
	{
		return Line (ID, ""+v);
	}
	
	public DebugOverlay Line(string ID, string text)
	{
		_texts[ID] = text;
		return this;
	}
	
	public DebugOverlay Bar(string ID, float fillRatio)
	{
		_bars[ID] = fillRatio;
		return this;
	}
	
	public DebugOverlay Histogram(float val)
	{
		if(_histogram == null)
		{
			_histogram = new DebugHistogram(200);
			_histogram.height = 50;
			_histogram.position = new Vector2(300f,10f);
		}
		
		_histogram.Push(val);
		return this;
	}
	
	void Clear()
	{
		_texts.Clear();
		_histogram = null;
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F3))
		{
			show = !show;
		}
	}
	
	void OnGUI()
	{
		if(!show)
			return;
		
		// Draw text
		Rect rect = new Rect(8, 8, 200, 20);
		GUI.color = Color.black;
		foreach(KeyValuePair<string,string> entry in _texts)
		{
			GUI.Label(rect, entry.Key + ": " + entry.Value, _style);
			rect.y += 20f;
		}
		
		// Draw bars
		foreach(KeyValuePair<string,float> bar in _bars)
		{
			GUI.color = Color.black;
			GUI.Label(rect, bar.Key+":");
			
			rect.x += 100f;
			
			GUI.color = new Color(0f,0f,0f,0.5f);
			GUI.DrawTexture(rect, tex);
			
			GUI.color = new Color(1f,1f,1f,0.75f);
			GUI.DrawTexture(new Rect(rect.x+1f, rect.y+1f, rect.width*bar.Value-2f, rect.height-2f), tex);
			
			rect.x -= 100f;
			rect.y += 20f;
		}
		
		// Draw histogram
		if(_histogram != null)
		{
			_histogram.OnGUI();
		}
	}
}

class DebugHistogram
{
	public Vector2      position;
	public int          height;
	private FloatRange  _valueRange = new FloatRange(0f, 0f);
	private float[]     _values;
	private int         _index;
	private Texture2D   _tex;
	
	public DebugHistogram(int size)
	{
		_values = new float[size];
				
		_tex = new Texture2D(2, 2, TextureFormat.ARGB32, false, false);
		_tex.anisoLevel = 1;
		_tex.filterMode = FilterMode.Point;
		_tex.SetPixel(0,0,new Color(1f,1f,1f));
		_tex.SetPixel(1,0,new Color(1f,1f,1f));
		_tex.SetPixel(1,1,new Color(1f,1f,1f));
		_tex.SetPixel(0,1,new Color(1f,1f,1f));
		_tex.Apply();
	}
	
	public void Push(float val)
	{
		_values[_index] = val;
		++_index;
		if(_index == _values.Length)
		{
			_index = 0;
		}
	}
	
	public void OnGUI()
	{
		GUI.color = new Color(0f,0f,0f,0.5f);
		GUI.DrawTexture(new Rect(position.x-1, position.y-1, _values.Length+2, height+2), _tex);
		
		float min = _values[0];
		float max = _values[0];
		for(int i = 0; i < _values.Length; ++i)
		{
			if(_values[i] < min) min = _values[i];
			if(_values[i] > max) max = _values[i];
		}
		if(Mathf.Approximately(min, max))
		{
			max += 0.01f;
		}
		_valueRange.min = min;
		_valueRange.max = max;
		
		GUI.color = new Color(1f,1f,1f,0.75f);
		for(int i = 0; i < _values.Length; ++i)
		{
			float h = ((float)height)*(0.98f*_valueRange.InverseLerp(_values[i])+0.02f);
			float x = position.x + i;
			float y = position.y + height;
			GUI.DrawTexture(new Rect(x, y-h, 1, h), _tex);
		}
	}

}


