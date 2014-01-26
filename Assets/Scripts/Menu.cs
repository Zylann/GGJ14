using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Menu : MonoBehaviour {

	//public GUIStyle gss;
	public GUISkin gsk;
	public GUISkin gsubsk;
	public GUISkin gpopup;
	public GUISkin GScoreSkn;
	
	public Texture backgroundCreditTexture;
	public Texture backgroundMenuTexture;
	public Texture BlackbackgroundTexture;
	public Texture HowToPlayTexture;
	
	public string sceneName;
	
	string zurl = "";
	
	int ButtonWidth,ButtonHeight;
	int ButtonPosX,ButtonPosY;
	//int ButtonW;
	//int ButtonH;
	int LabelW;
	int LabelH;
	
	
	bool HowToPlayisActive = false;
	bool SettingsisActive = false;
	bool HighScoreisActive = false;
	bool CreditsisActive = false;
	bool PopupActive = false;
	
	GUI play;
	SaveManager sm;
	
	private List<string> topDix;
	
	public Dictionary<string, int> scores = new Dictionary<string, int>();
	
	// Use this for initialization
	void Start () 
	{
		ButtonHeight = (Screen.height/16)/*/ - Screen.height/3)/5*/;
		ButtonWidth = (Screen.width/6);

		ButtonPosX = Screen.width/2 - ButtonWidth /2;
		ButtonPosY = Screen.height/8;
		
		//ButtonW = Screen.width/20;
		//ButtonH = Screen.width/20;
	
		sm = SaveManager.Get();
		sm.LoadGame();
		//sm.SaveGame();
		
		scores = SaveManager.Get().getHightScore();
		topDix = new List<string>();
		
		
		foreach(string key in scores.Keys)
		{
			topDix.Add(key+"/ : "+scores[key]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.skin = gsk;
		
		
		int ButtonW = Screen.width/30;
		int ButtonH = Screen.width/30;
		
		if(HowToPlayisActive) //HowToPlay
		{
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), HowToPlayTexture, ScaleMode.StretchToFill);
			
			//Bouton Retour
			GUI.skin = gpopup;
			//Bouton Retour
			if(GUI.Button(new Rect(Screen.width*14/15, Screen.height*1/15,ButtonW,ButtonH), "X"))
			{
				 HowToPlayisActive = false;
			}
		}
		else if(CreditsisActive) //Credits
		{
			GUI.skin = gsk;
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundCreditTexture, ScaleMode.StretchToFill);
			
			//Bouton Retour
			GUI.skin = gpopup;
			//Bouton Retour
			if(GUI.Button(new Rect(Screen.width*14/15, Screen.height*1/15,ButtonW,ButtonH), "X"))
			{
				 CreditsisActive = false;
			}
			
			GUI.skin = gsk;
			GUI.Label(new Rect(0,Screen.height/6,Screen.width/2,Screen.height/10), "Credits");
			GUI.Label(new Rect(0,Screen.height/5*2,Screen.width/2,Screen.height/10), "Vincent Abry");
			GUI.Label(new Rect(0,Screen.height/5*3,Screen.width/2,Screen.height/10), "Marc Gilleron");
			GUI.Label(new Rect(0,Screen.height/5*4,Screen.width/2,Screen.height/10), "Elizabeth Maler");
			GUI.Label(new Rect(Screen.width/2,Screen.height/5*2,Screen.width/2,Screen.height/10), "Théo Torregrossa");
			GUI.Label(new Rect(Screen.width/2,Screen.height/5*3,Screen.width/2,Screen.height/10), "Arnaud Millot");
			GUI.Label(new Rect(Screen.width/2,Screen.height/5*4,Screen.width/2,Screen.height/10), "Benjamin Teissier");
		}
		else if(SettingsisActive) //Settings
		{
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundCreditTexture, ScaleMode.StretchToFill);


			GUI.skin = gsubsk;
			gsubsk.GetStyle("Label").fontSize=Screen.height/20;
			gsubsk.GetStyle("TextField").fontSize=Screen.width/50;

			//Label
			GUI.Label(new Rect(Screen.width/6,Screen.height/4 +Screen.height/18,Screen.width/3*2,300), "Please enter the absolute adress of a square .png picture");
			
			//Champ de saisie
			zurl = GUI.TextField(new Rect(Screen.width/5,Screen.height/4, Screen.width/2, Screen.height/18), zurl, 300);
			string path = "file://";
	
	        path += zurl;
			zurl.Replace('\\', '/');
	
	        WWW www = new WWW(path);
	
			//Bouton OK
			GUI.skin = gpopup;
			gpopup.GetStyle("Button").fontSize=Screen.width/60;
			if(GUI.Button(new Rect(Screen.width/5 + Screen.width/2 +5,Screen.height/4, Screen.height/16, Screen.height/18), "OK"))
			{
				StartCoroutine(WaitForRequest(www));
			}

			
			//Bouton Retour
			if(GUI.Button(new Rect(Screen.width*14/15, Screen.height*1/15,ButtonW,ButtonH), "X"))
			{
				SettingsisActive = false;
				PopupActive = false;
			}
			
			//PopUp
			if (PopupActive)
			{
				GUI.Label(new Rect(Screen.width/6,Screen.height/6,2*Screen.width/3,2*Screen.height/3), BlackbackgroundTexture);
				if(GUI.Button(new Rect(Screen.width/2-30,Screen.height/3+Screen.height/3*9/10,60,30), "It work !"))
				{
					PopupActive = false;
				}
			}
		}
		else if(HighScoreisActive) //HighScore
		{
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundCreditTexture, ScaleMode.StretchToFill);

			GUI.skin = gsk;
			gsk.GetStyle("Label").fontSize=Screen.height/15;
			GUI.Label(new Rect(0, Screen.height/8,Screen.width,Screen.height/10), "Scores");
			GUI.skin = GScoreSkn;
			GScoreSkn.GetStyle("Label").fontSize=Screen.height/20;
			//Debug.Log("foo"+topDix.Count);
			
			int j = 1;
			for (int i = topDix.Count-1; i > (topDix.Count-6); --i)
			{
				GUI.Label(new Rect(0,Screen.height/6+Screen.height/10*j,Screen.width,Screen.height/10), topDix[i]);
				j++;
			}
			
			//Bouton Retour
			GUI.skin = gpopup;
			if(GUI.Button(new Rect(Screen.width*14/15, Screen.height*1/15,ButtonW,ButtonH), "X"))
			{
				 HighScoreisActive = false;
			}
		}
		else
		{
			GUI.skin = gsk;
			gsk.GetStyle("Button").fontSize=Screen.width/40;

			
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundMenuTexture, ScaleMode.ScaleAndCrop);
			
			if(GUI.Button(new Rect(ButtonPosX, ButtonPosY,ButtonWidth,ButtonHeight), "Play"))
			{
				 Application.LoadLevel (sceneName); 
			}
			
			if(GUI.Button(new Rect(ButtonPosX, ButtonPosY+ButtonHeight*2,ButtonWidth,ButtonHeight), "How to play"))
			{
				 HowToPlayisActive = true;
				
			}
			
			if(GUI.Button(new Rect(ButtonPosX, ButtonPosY+ButtonHeight*4,ButtonWidth,ButtonHeight), "Settings"))
			{
				SettingsisActive = true;
			}
			
			if(GUI.Button(new Rect(ButtonPosX,ButtonPosY+ButtonHeight*6,ButtonWidth,ButtonHeight), "High scores"))
			{
				HighScoreisActive = true;
			}
			
			if(GUI.Button(new Rect(ButtonPosX,ButtonPosY+ButtonHeight*8,ButtonWidth,ButtonHeight), "Credits"))
			{
		
				CreditsisActive = true;
			}
			
			if(GUI.Button(new Rect(ButtonPosX,ButtonPosY+ButtonHeight*10,ButtonWidth,ButtonHeight), "Quit"))
			{
				Application.Quit();
			}
		}
	}
	
	private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // Check for errors
        if (www.error == null)
        {
            Debug.Log("Texture load OK");
			
			PopupActive = true;
			GameObject.Find("canard").GetComponent<GUITexture>().texture = www.texture;
        }
        else
        {
            Debug.Log("Texture load error: "+ www.error);
        }
    }
}