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
	
	public int ButtonWidth,ButtonHeight;
	int ButtonPosX,ButtonPosY;
	int ButtonW;
	int ButtonH;
	int LabelW;
	int LabelH;
	
	
	bool HowToPlayisActive = false;
	bool SettingsisActive = false;
	bool HightScoreisActive = false;
	bool CreditsisActive = false;
	bool PopupActive = false;
	
	GUI play;
	SaveManager sm;
	
	private List<string> topDix;
	
	public Dictionary<string, int> scores = new Dictionary<string, int>();
	
	// Use this for initialization
	void Start () 
	{
		ButtonPosX = Screen.width/2 - ButtonWidth /2;
		ButtonPosY = Screen.height/2 - ButtonHeight*6 + ButtonHeight/2;
		
		ButtonW = Screen.width/20;
		ButtonH = Screen.width/20;
	
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
		
		int ButtonW = Screen.width/20;
		int ButtonH = Screen.width/20;
		
		if(HowToPlayisActive) //HowToPlay
		{
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), HowToPlayTexture, ScaleMode.StretchToFill);
			
			//Bouton Retour
			GUI.skin = gpopup;
			if(GUI.Button(new Rect(Screen.width*9/10, Screen.height*1/10,ButtonW,ButtonH), "X"))
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
			if(GUI.Button(new Rect(Screen.width*9/10, Screen.height*1/10,ButtonW,ButtonH), "X"))
			{
				 CreditsisActive = false;
			}
			
			GUI.skin = gsk;
			GUI.Label(new Rect(Screen.width/8,Screen.height/6,100,30), "Crédit");
			GUI.Label(new Rect(Screen.width/5,Screen.height/5*2,100,30), "Grand fou 1");
			GUI.Label(new Rect(Screen.width/5,Screen.height/5*3,100,30), "Grand fou 2");
			GUI.Label(new Rect(Screen.width/5,Screen.height/5*4,100,30), "Grand fou 3");
			GUI.Label(new Rect(Screen.width/4*2,Screen.height/5*2,100,30), "Grand fou 4");
			GUI.Label(new Rect(Screen.width/4*2,Screen.height/5*3,100,30), "Grand fou 5");
			GUI.Label(new Rect(Screen.width/4*2,Screen.height/5*4,100,30), "Grand fou 6");
		}
		else if(SettingsisActive) //Settings
		{
			GUI.skin = gsubsk;
			
			
			//Label
			GUI.Label(new Rect(Screen.width/8,Screen.height/6,Screen.width/2,300), "Please enter the absolute adress of a square .png picture");
			
			//Champ de saisie
			zurl = GUI.TextField(new Rect(Screen.width/8,Screen.height/6+Screen.height/6, Screen.width/2, 30), zurl, 300);
			string path = "file://";
	
	        path += zurl;
			zurl.Replace('\\', '/');
	
	        WWW www = new WWW(path);
	
			//Bouton OK
			GUI.skin = gpopup;
			if(GUI.Button(new Rect(Screen.width/8 + Screen.width/2 +5,Screen.height/6+Screen.height/6, 30, 30), "OK"))
			{
				StartCoroutine(WaitForRequest(www));
			}
			
			//Bouton Retour
			if(GUI.Button(new Rect(Screen.width*9/10, Screen.height*1/10,ButtonW,ButtonH), "X"))
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
		else if(HightScoreisActive) //HightScore
		{
			GUI.skin = gsk;
			GUI.Label(new Rect(Screen.width/2 -50,Screen.height/8,100,30), "Scores");
			GUI.skin = GScoreSkn;
			
			//Debug.Log("foo"+topDix.Count);
			
			int j = 1;
			for (int i = topDix.Count-1; i > (topDix.Count-6); --i)
			{
				GUI.Label(new Rect(Screen.width/2 - 50,Screen.height/6+30*j,100,30), topDix[i]);
				j++;
			}
			
			//Bouton Retour
			if(GUI.Button(new Rect(Screen.width*9/10, Screen.height*1/10,ButtonW,ButtonH), "X"))
			{
				 HightScoreisActive = false;
			}
		}
		else
		{
			GUI.skin = gsk;
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
			
			if(GUI.Button(new Rect(ButtonPosX,ButtonPosY+ButtonHeight*6,ButtonWidth,ButtonHeight), "Hight scores"))
			{
				HightScoreisActive = true;
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