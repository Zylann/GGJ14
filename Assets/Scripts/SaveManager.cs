using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SaveManager
{
	
	private static SaveManager instance = null;
	public Dictionary<string, int> scores = new Dictionary<string, int>();
	public string path;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static SaveManager Get()
    {
        if (instance == null)
            instance = new SaveManager();
        return instance;
    }
	
	public void SaveGame()
	{
		path = Application.dataPath.ToString() + "/Save";
		string text = "";
		//Check for directory
		
		if(!Directory.Exists(path)) 
		{
        	Directory.CreateDirectory (path);
    	}
		//TODO creer string
		foreach(string key in scores.Keys)
		{
			text+=":"+key+"/"+scores[key];
		}
		
		for (int i = 0; i < 10; ++i)
		{
			text+=":---"+i+"/0";
		}
		//Debug.Log (text);
		
		
		//Write to file
		System.IO.File.WriteAllText(path + "/save.save", text);
	}
	
	public void LoadGame()
	{
		path = Application.dataPath.ToString() + "/Save";
		
		if(!System.IO.File.Exists(path + "/save.save"))
		{
			return;
		}
		string saveFile = System.IO.File.ReadAllText(path + "/save.save");
		
		//TODO parser le string
		string[] split_personne = saveFile.Split(':');
		
		int i = 0;
		foreach( string str in split_personne)
		{
			if(str!="")
			{
				string[] split_scores = str.Split('/');
				if(scores.ContainsKey(split_scores[0]))
				{
					
					scores.Add(split_scores[0]+i, int.Parse(split_scores[1]));
					++i;
				}
				else scores.Add(split_scores[0], int.Parse(split_scores[1]));
			}
		}
	}
	
	public Dictionary<string, int> getHightScore()
	{
		scores = scores.OrderBy(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        
		
		Dictionary<string, int> LesScores = new Dictionary<string, int>();
		
		LesScores.Clear();
		
		
        foreach (KeyValuePair<string, int> kvp in scores)
			LesScores.Add(kvp.Key, kvp.Value);
		
		return LesScores;
	}
}
