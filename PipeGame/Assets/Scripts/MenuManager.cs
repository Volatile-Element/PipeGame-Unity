using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
		
	ReadWrite rwInstance;

	public void ChangeScene(string level)
	{
		Debug.Log (level);
		rwInstance = FindObjectOfType<ReadWrite> ();
		Application.LoadLevel ("Levels");
		ReadWrite.selection = level;
	}
}
