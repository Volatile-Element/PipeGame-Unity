using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	public List<string> levelData = new List<string>();
	
	public int startNodeLoc;
	public int endNodeLoc;
	
	public int numOfStraight;
	public int numOfCorner;
	public int numOfThree;
	public int numOfFour;
	public int numOfBridge;
	public int numOfCornerBridge;
	public int numOfPumpOne;

}
