// Creates or rewrites a .txt file for each .json file in the same folder
// whenever the .resx changes
using UnityEditor;
using UnityEngine;
using System.IO;

public class CustomJSONImporter : AssetPostprocessor
{
	//private const string ATLAS_FOLDER = "Atlases/";
	//private const string MAP_FOLDER = "Maps/";
	
	public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		//Debug.Log("zPlane JSON importer begin...");
		
		foreach (string assetPath in importedAssets)
		{
			if(assetPath.EndsWith(".json"))
			{
				//
				// Make a textAsset out of it, so it becomes readable
				//
				
				string assetDirectory = assetPath.Substring (0, assetPath.Length - System.IO.Path.GetFileName (assetPath).Length) /*+ "Generated Assets/"*/;
				string assetName = System.IO.Path.GetFileNameWithoutExtension (assetPath);
				string newFileName = assetDirectory + assetName + ".txt";

				if (!Directory.Exists (assetDirectory))
				{
					Directory.CreateDirectory (assetDirectory);
				}

				StreamReader reader = new StreamReader (assetPath);
				string fileData = reader.ReadToEnd ();
				reader.Close ();
     
				FileStream resourceFile = new FileStream (newFileName, /*FileMode.Truncate |*/ FileMode.OpenOrCreate, FileAccess.Write);
				StreamWriter writer = new StreamWriter (resourceFile);
				writer.Write (fileData);
				writer.Close ();
				resourceFile.Close ();
     
				AssetDatabase.Refresh (ImportAssetOptions.Default);
			}
		}
		
		//Debug.Log("zPlane JSON importer done.");
	}

}

