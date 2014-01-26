using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MapEditorReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(!Application.isPlaying)
		{
			Debug.Log("Reset Map.levelIndex");
			Map.levelIndex = 0;
		}
	}
	
}
