using UnityEngine;
using System.Collections;

public class DuckVision : MonoBehaviour
{
	public Camera maskCamera;
	//public Camera duckLandCamera;
	public Material mapMaterial;
	private RenderTexture _maskRT;

	void Start ()
	{
		_maskRT = new RenderTexture(Screen.width, Screen.height, 16);
		maskCamera.targetTexture = _maskRT;
		mapMaterial.SetTexture("_Mask", _maskRT);
	}

}

