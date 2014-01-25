using UnityEngine;
using System.Collections;

public class DuckVision : MonoBehaviour
{
	public Camera maskCamera;
	public Camera duckLandCamera;
	public Material mixMaterial;
	private RenderTexture _maskRT;
	private RenderTexture _duckRT;

	void Start ()
	{
		_maskRT = new RenderTexture(Screen.width, Screen.height, 16);
		maskCamera.targetTexture = _maskRT;

		_duckRT = new RenderTexture(Screen.width, Screen.height, 16);
		duckLandCamera.targetTexture = _duckRT;

		mixMaterial.SetTexture("_Mask", _maskRT);
		mixMaterial.SetTexture("_HiddenTexture", _duckRT);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit(src, dst, mixMaterial);
	}

}

