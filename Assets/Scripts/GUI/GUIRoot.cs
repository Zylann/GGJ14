using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))] [ExecuteInEditMode]
public class GUIRoot : MonoBehaviour
{
	private static GUIRoot __instance;
	public static GUIRoot Instance { get { return __instance; } }
		
	private Camera _guiCamera;
	private int _lastScreenWidth;
	private int _lastScreenHeight;
	
	void Awake()
	{
		__instance = this;
	}
	
	void Start ()
	{
	}
	
	void Update ()
	{
		if(_lastScreenWidth != Screen.width || _lastScreenHeight != Screen.height)
		{
			//Debug.Log("Screen size changed");
			BroadcastMessage("OnScreenSizeChanged", SendMessageOptions.DontRequireReceiver);
		}
		
		_lastScreenWidth = Screen.width;
		_lastScreenHeight = Screen.height;
	}
	
	public Camera guiCamera
	{
		get
		{
			if(_guiCamera == null)
				_guiCamera = this.camera;
			return _guiCamera;
		}
	}
	
	public Vector3 WorldToGUI(Vector3 position)
	{
		Camera cam = Camera.main;
		Camera guiCam = guiCamera;
		
		position = cam.WorldToScreenPoint(position);
		position = guiCam.ScreenToWorldPoint(new Vector3(position.x, position.y, guiCam.nearClipPlane+1f));
		
		Vector3 scale = transform.localScale;
		
		return new Vector3(position.x/scale.x, position.y/scale.y, position.z/scale.z);
	}
	
	void OnDrawGizmos()
	{
		float ratio = camera.aspect;
		float size = 2f*camera.orthographicSize;
		Gizmos.color = new Color(0.5f, 0f, 0.5f);
		Gizmos.DrawWireCube(transform.position, new Vector3(size*ratio, size, 0f));
	}

}


