using UnityEngine;
using System.Collections;

[System.Serializable]
public class IntRange
{
	public int min;
	public int max;
        
	public IntRange (int pmin, int pmax)
	{
		min = pmin;
		max = pmax;
	}
        
	public int Rand ()
	{
		return UnityEngine.Random.Range (min, max);
	}
	
	public int width
	{
		get { return max - min + 1; }
	}
	
	public int Clamp(int val)
	{
		if(val > max) return max;
		if(val < min) return min;
		return val;
	}
	
	public void CopyFrom(IntRange other)
	{
		min = other.min;
		max = other.max;
	}

}

[System.Serializable]
public class FloatRange
{
	public float min;
	public float max;
	
	public FloatRange(FloatRange other)
	{
		min = other.min;
		max = other.max;
	}
	
	public FloatRange(float pmin, float pmax)
	{
		min = pmin;
		max = pmax;
	}
    
	public float Rand()
	{
		return UnityEngine.Random.Range (min, max);
	}
	
	public float width
	{
		get { return max - min; }
	}
	
	public float GetLerp(float ratio)
	{
		return min + (max - min) * ratio; 
	}
	
	public float InverseLerp(float val)
	{
		return Mathf.Clamp01((val - min) / width);
	}
	
	public float Clamp(float val)
	{
		if(val > max) return max;
		if(val < min) return min;
		return val;
	}
	
	public void CopyFrom(FloatRange other)
	{
		min = other.min;
		max = other.max;
	}
	
	public void SetFromMix(FloatRange a, FloatRange b, float t)
	{
		min = Mathf.Lerp(a.min, b.min, t);
		max = Mathf.Lerp(a.max, b.max, t);
	}

}
