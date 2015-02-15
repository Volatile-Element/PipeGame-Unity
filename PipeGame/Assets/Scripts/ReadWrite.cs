using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class ReadWrite : MonoBehaviour {

	public string currentLevelData;
	public Level levelObject;

	void Start() 
	{
		levelObject = new Level();
		GetLevel (1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void GetLevel(int selection)
	{
		currentLevelData = Resources.Load("Level " + selection).ToString();
		Debug.Log (currentLevelData);

		LevelBuilder ();
	}

	public void LevelBuilder()
	{
		string value = "";
		bool nextIsValue = false;
		int counter = 0;
		foreach (char currentChar in currentLevelData) 
		{
			if (currentChar == ',')
			{
				levelObject.levelData.Add(value);
				Debug.Log(value);
				value = "";
				nextIsValue =false;
			}
			if (currentChar == 'S')
			{
				levelObject.startNodeLoc = counter;
			}
			if (currentChar == 'E')
			{
				levelObject.endNodeLoc = counter;
			}
			if (nextIsValue == true)
			{
				value += currentChar;
			}
			if (currentChar == ':')
			{
				nextIsValue = true; 
			}
			counter++;
		}
	}

}
