using UnityEngine;
using System.Collections;

public class FlowManager : MonoBehaviour {

    // Sets start tile for flow to start
    public int[] startTile = { 0, 0 };
    int[] currentTile;
    public int startDirection = 1;

    int fromDirection;

    TileInteraction tile;

    
    public void startFlow()
    {
        int direction = -1;

        bool found = false;

        switch (startDirection)
        {
            case 0:
                direction = 2;
                break;
            case 1:
                direction = 3;
                break;
            case 2:
                direction = 0;
                break;
            case 3:
                direction = 1;
                break;
        }

        for (int i = 0; i < tile.in_Out.Length; i++)
        {
            if (tile.in_Out[i] == direction)
            {
                found = true;
            }
        }

        if (found == true)
        {

        }
        else if (found == false)
        {

        }
    }

    void flowing()
    {

    }

    void nextTile(int[] currentTies)
    {

    }

	// Use this for initialization
	void Start () 
    {
        // Gets the TileInteraction of the starting tile
        tile = GameObject.Find("x:" + startTile[0] + " y:" + startTile[1]).GetComponent<TileInteraction>();

        // Sets current tile to the start tile
        currentTile = startTile;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
