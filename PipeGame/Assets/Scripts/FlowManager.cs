using UnityEngine;
using System.Collections;

public class FlowManager : MonoBehaviour {

    // Sets start tile for flow to start
    public int[] startTile = { 0, 0 };
    int[] currentTile;
    public int startDirection = 1;

    int fromDirection;

    TileInteraction tile;
    GridManager gm;

    
    public void startFlow() // Starting the flow from the first tile
    {
        int direction = -1;

        bool found = false;
        currentTile[0] = startTile[0]; // Setting the current tile set
        currentTile[1] = startTile[1];

        switch (startDirection) // Deciding the start direction to look for on the first tile
        {
            case 0:
                direction = 2;
                fromDirection = 2;
                break;
            case 1:
                direction = 3;
                fromDirection = 3;
                break;
            case 2:
                direction = 0;
                fromDirection = 0;
                break;
            case 3:
                direction = 1;
                fromDirection = 1;
                break;
        }

        for (int i = 0; i < tile.in_Out.Length; i++) // Finding the first tile set
        {
            if (tile.in_Out[i] == direction)
            {
                found = true;
            }
        }

        if (found == true)
        {
            Debug.Log("Passed");
            StartCoroutine(nextMove());
        }
        else if (found == false)
        {
            Debug.Log("Player Fail");
        }
    }

    void flowing()
    {
        Debug.Log("Next tile is X:" + currentTile[0] + " Y:" + currentTile[1]);
        StartCoroutine(nextMove());
    }

    void nextTile()
    {
        tile.GetComponent<SpriteRenderer>().sprite = gm.tileSprites[2];

        switch (fromDirection)
        {
            case 0: // From top
                if (tile.rightB == true)
                {
                    currentTile[0] = currentTile[0] + 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 3;
                    flowing();
                }
                else if (tile.downB == true)
                {
                    currentTile[1] = currentTile[1] - 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 0;
                    flowing();
                }
                else if (tile.leftB == true)
                {
                    currentTile[0] = currentTile[0] - 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 1;
                    flowing();
                }
                else
                {
                    Debug.Log("Player Failed");
                }
                break;
            case 1: // From right
                if (tile.upB == true)
                {
                    currentTile[1] = currentTile[1] + 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 2;
                    flowing();
                }
                else if (tile.downB == true)
                {
                    currentTile[1] = currentTile[1] - 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 0;
                    flowing();
                }
                else if (tile.leftB == true)
                {
                    currentTile[0] = currentTile[0] - 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 1;
                    flowing();
                }
                else
                {
                    Debug.Log("Player Failed");
                }
                break;
            case 2: // From bottom
                if (tile.upB == true)
                {
                    currentTile[1] = currentTile[1] + 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 2;
                    flowing();
                }
                else if (tile.leftB == true)
                {
                    currentTile[0] = currentTile[0] - 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 1;
                    flowing();
                }
                else if (tile.rightB == true)
                {
                    currentTile[0] = currentTile[0] + 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 3;
                    flowing();
                }
                else
                {
                    Debug.Log("Player Failed");
                }
                break;
            case 3: // From Left
                if (tile.upB == true)
                {
                    currentTile[1] = currentTile[1] + 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 2;
                    flowing();
                }
                else if (tile.downB == true)
                {
                    currentTile[1] = currentTile[1] - 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 0;
                    flowing();
                }
                else if (tile.rightB == true)
                {
                    currentTile[0] = currentTile[0] + 1;
                    tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();
                    fromDirection = 3;
                    flowing();
                }
                break;
            default:
                Debug.Log("Player Failed");
                break;
        }
    }

	// Use this for initialization
	void Start () 
    {
        // Gets the TileInteraction of the starting tile
        tile = GameObject.Find("x:" + startTile[0] + " y:" + startTile[1]).GetComponent<TileInteraction>();

        gm = GameObject.FindObjectOfType<GridManager>();

        // Sets current tile to the start tile
        currentTile = startTile;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator nextMove()
    {
        yield return new WaitForSeconds(5.0f);
        nextTile();
    }
}
