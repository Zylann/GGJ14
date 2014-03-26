using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IgMenu : MonoBehaviour
{
	public enum IG_MENU_STATE { NONE, NEXT_LEVEL }
	public IG_MENU_STATE _menu_state;

	private float _button_width = 0.3f;
	private float _button_height = 0.10f;
	private float _left_margin = 0.4f;
	private float _top_margin = 0.25f;

	public Texture popUpTexture;
	
	public GUISkin gsk;


	int ButtonWidth,ButtonHeight;
	int ButtonPosX,ButtonPosY;
	//int ButtonW;
	//int ButtonH;
	int LabelW;
	int LabelH;


	private List<string> topDix;


	public void OnGUI()
	{
		GUI.skin = gsk;
		ScreenHelper scr = Game.Inst.m_screen_helper;

		int ButtonW = Screen.width/30;
		int ButtonH = Screen.width/30;

		switch (_menu_state)
		{
		case IG_MENU_STATE.NONE:
			Screen.showCursor = false;

			break;
		case IG_MENU_STATE.NEXT_LEVEL:
			Screen.showCursor = true;


			SaveManager.Get().ScoreTotal += Game.Inst.m_scoring._current_score;

			GUI.Label(new Rect(Screen.width/2-Screen.width/3,Screen.height/2-Screen.height/3,2*Screen.width/3,2*Screen.height/3), popUpTexture);
			//if ! dernier niveau
			if(Map.levelIndex != 2)
			{
				if(GUI.Button(
					new Rect(scr.W_to_px(_left_margin), scr.H_to_px(_top_margin),
			         	 	scr.W_to_px(_button_width), scr.H_to_px(_button_height)),
						 	"Try Again"))
				{

					Map.Reload();
				}

				if(GUI.Button(
					new Rect(scr.W_to_px(_left_margin), scr.H_to_px(_top_margin + _button_height * 3f / 2f),
		                    scr.W_to_px(_button_width), scr.H_to_px(_button_height)),
		           			"Next Level"))
				{
					Map.LoadNext();
				}
			}
			else //AFFICHER LES SCORES
			{
				//Calculer et save

				/*GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundTexture, ScaleMode.StretchToFill);
				
				GUI.skin = gsk;
				gsk.GetStyle("Label").fontSize=Screen.height/15;
				GUI.Label(new Rect(0, Screen.height/8,Screen.width,Screen.height/10), "Scores");
				GUI.skin = GScoreSkn;
				GScoreSkn.GetStyle("Label").fontSize=Screen.height/20;
				
				int j = 1;
				for (int i = topDix.Count-1; i > (topDix.Count-6); --i)
				{
					topDix[i]=topDix[i].Replace('/', ' ');

					GUI.Label(new Rect(0,Screen.height/6+Screen.height/10*j,Screen.width,Screen.height/10), topDix[i]);
					j++;
				}*/
				
				//Bouton Retour
				//GUI.skin = gpopup;
				if(GUI.Button(new Rect(Screen.width/2-Screen.width/5/2, Screen.height/2-Screen.width/5/2,Screen.width/5, Screen.height/5), "You Win !!\n Score : " + SaveManager.Get().ScoreTotal))
				{
					playFeedback("Menu/Click");
					Map.levelIndex = 0;
					Application.LoadLevel("menu");
				}
			}

			if (Map.levelIndex == 0)
			{

				/*//Champ de saisie
				SaveManager.Get().playerName = GUI.TextField(new Rect(Screen.width/5,Screen.height/4, Screen.width/2, Screen.height/18), "Enter yourn name", 300);

				//Bouton OK
				//GUI.skin = gpopup;
				//gpopup.GetStyle("Button").fontSize=Screen.width/60;
				*/
				/*if(GUI.Button(new Rect(Screen.width/2,Screen.height/4, Screen.height/16, Screen.height/18), "You Win !! Score : " + SaveManager.Get().ScoreTotal))
				{
					playFeedback("Menu/Click");
					SaveManager.Get().scores.Add(SaveManager.Get().playerName, SaveManager.Get ().ScoreTotal);
					SaveManager.Get ().SaveGame();
				}*/

			}





			break;
		}
	}
	private void playFeedback(string str)
	{
		Fabric.EventManager.Instance.PostEvent(str);
	}

}
