using UnityEngine;
using System.Collections;

public class IgMenu : MonoBehaviour
{
	public enum IG_MENU_STATE { NONE, NEXT_LEVEL }
	public IG_MENU_STATE _menu_state;

	private float _button_width = 0.15f;
	private float _button_height = 0.10f;
	private float _left_margin = 0.4f;
	private float _top_margin = 0.25f;

	public void OnGUI()
	{
		ScreenHelper scr = Game.Inst.m_screen_helper;

		switch (_menu_state)
		{
		case IG_MENU_STATE.NONE:
			Screen.showCursor = false;

			break;
		case IG_MENU_STATE.NEXT_LEVEL:
			Screen.showCursor = true;

			GUI.Button(
				new Rect(scr.W_to_px(_left_margin), scr.H_to_px(_top_margin),
		         	 	scr.W_to_px(_button_width), scr.H_to_px(_button_height)),
					 	"Try Again");

			GUI.Button(
				new Rect(scr.W_to_px(_left_margin), scr.H_to_px(_top_margin + _button_height * 3f / 2f),
	                    scr.W_to_px(_button_width), scr.H_to_px(_button_height)),
	           			"Next Level");

			break;
		}
	}
}
