using UnityEngine;
using System.Collections;

/// <summary>
/// This script automatically adjusts the position of its GameObject
/// to match a position on the screen specified as a ratio, so it 
/// adapts to screen resolution changes
/// </summary>
[ExecuteInEditMode]
public class GUIAnchor : MonoBehaviour
{
	public Vector2 align = new Vector2(0f, 0f);
	public float anchorScale = 5f; // JAM CODE
	
	void OnScreenSizeChanged()
	{
		int width = Screen.width;
		int height = Screen.height;
		
		float ratio = (float)width / (float)height;

		Vector3 position = new Vector3(
			align.x * (float)ratio * anchorScale,
			align.y * anchorScale,
			transform.localPosition.z
		);

		transform.localPosition = position;
	}

#if UNITY_EDITOR
	public bool update = true;
	void Update()
	{
		if(update)
		{
			OnScreenSizeChanged();
			update = false;
		}
	}
#endif
	
}


