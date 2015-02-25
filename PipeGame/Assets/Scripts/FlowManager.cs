using UnityEngine;
using System.Collections;

/*
 * Copyright Volatile Element 2014
 * 
 * Methods:
 * Start()
 * StartFlow()
 * NextTile()
 * NextTileSorter(int nextDir)
 * NextMove()
 * 
 * Coders:
 * Matthew Moore (Creator)
 */

/// <summary>
/// Starts the flow when the class is initialized
/// Chooses while tile the flow will move to next
/// </summary>
public class FlowManager : MonoBehaviour 
{

    // Sets start tile for flow to start
    public int[] startTile = { 0, 0 };
    int[] currentTile = new int[2];
    public int startDirection = 1;

    int fromDirection;

    TileInteraction tile;
    GridManager gm;

    bool canFlow = true;

    /// <summary>
    /// Starts flow from the start tile in the start direction
    /// </summary>
    public void StartFlow()
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

        for (int i = 0; i < tile.in_Out1.Length; i++) // Finding the first tile set
        {
            if (tile.in_Out1[i] == direction)
            {
                found = true;
            }
        }

        if (found == true)
        {
            Debug.Log("Passed");
            StartCoroutine(NextMove());
        }
        else if (found == false)
        {
            gm.FailedGame("StartFlow 62");
        }
    }

    /// <summary>
    /// Decides which tile is the next tile for the flow to go to depending on the current tile type
    /// </summary>
    void NextTile()
    {
        // Changes the sprite of the tile
        tile.GetComponent<SpriteRenderer>().sprite = gm.tileSprites[2];

        if (tile.tileTypeString == "Straight")
        {
            if (fromDirection == tile.in_Out1[0])
            {
                NextTileSorter(tile.in_Out1[1]);
            }
            else if (fromDirection == tile.in_Out1[1])
            {
                NextTileSorter(tile.in_Out1[0]);
            }
            else
            {
                gm.FailedGame("nextTile 89");
            }
        }

        if (tile.tileTypeString == "Bridge")
        {
            if (fromDirection == tile.in_Out1[0])
            {
                NextTileSorter(tile.in_Out1[1]);
            }
            else if (fromDirection == tile.in_Out1[1])
            {
                NextTileSorter(tile.in_Out1[0]);
            }
            else if (fromDirection == tile.in_Out2[0])
            {
                NextTileSorter(tile.in_Out2[1]);
            }
            else if (fromDirection == tile.in_Out2[1])
            {
                NextTileSorter(tile.in_Out2[0]);
            }
            else
            {
                gm.FailedGame("nextTile 113");
            }
        }
    }

    /// <summary>
    /// Gets the next tile to flow to
    /// Decides if the player has hit a fail state or a win state
    /// </summary>
    /// <param name="nextDir">Direction of the out from the last tile</param>
    void NextTileSorter(int nextDir)
    {
        switch (nextDir)
        {
            case 0: // Up
                if (tile.name == "x:" + gm.winTile[0] + " y:" + gm.winTile[1])
                {
                    if (nextDir == gm.winDir)
                    {
                        gm.WinGame("NextTileSorter 106");
                    }
                    else
                    {
                        gm.FailedGame("NextTileSorter 110");
                    }
                }
                else
                {
                    if (tile.upB == true) // Checks to make sure the current tile can flow upwards
                    {
                        currentTile[1] = currentTile[1] + 1;
                        tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>(); // Gets the new tile

                        if (tile.isPump != true && tile.tileTypeString != "Split")
                        {
                            fromDirection = 2;

                            if (tile.downB == true)
                            {
                                Debug.Log("Next tile is X:" + currentTile[0] + " Y:" + currentTile[1]);
                                StartCoroutine(NextMove());
                            }
                            else
                            {
                                gm.FailedGame("NextTileSorter 115");
                            }
                        }
                        else if (tile.tileTypeString == "Split")
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = gm.tileSprites[2];
                            tile.splitIn = true;
                            tile.splitInFlow = 2;
                        }
                        else if (tile.isPump == true)
                        {
                            tile.pumpIns++;
                        }
                    }
                }
                break;
            case 1: // Right
                if (tile.name == "x:" + gm.winTile[0] + " y:" + gm.winTile[1])
                {
                    if (nextDir == gm.winDir)
                    {
                        gm.WinGame("NextTileSorter 142");
                    }
                    else
                    {
                        gm.FailedGame("NextTileSorter 146");
                    }
                }
                else
                {
                    if (tile.rightB == true)
                    {
                        currentTile[0] = currentTile[0] + 1;
                        tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();

                        if (tile.isPump != true && tile.tileTypeString != "Split")
                        {
                            fromDirection = 3;

                            if (tile.leftB == true)
                            {
                                Debug.Log("Next tile is X:" + currentTile[0] + " Y:" + currentTile[1]);
                                StartCoroutine(NextMove());
                            }
                            else
                            {
                                gm.FailedGame("NextTileSorter 137");
                            }
                        }
                        else if (tile.tileTypeString == "Split")
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = gm.tileSprites[2];
                            tile.splitIn = true;
                            tile.splitInFlow = 2;
                        }
                        else if (tile.isPump == true)
                        {
                            tile.pumpIns++;
                        }
                    }
                }
                break;
            case 2: // Down
                if (tile.name == "x:" + gm.winTile[0] + " y:" + gm.winTile[1])
                {
                    if (nextDir == gm.winDir)
                    {
                        gm.WinGame("NextTileSorter 179");
                    }
                    else
                    {
                        gm.FailedGame("NextTileSorter 183");
                    }
                }
                else
                {
                    if (tile.downB == true)
                    {
                        currentTile[1] = currentTile[1] - 1;
                        tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();

                        if (tile.isPump != true && tile.tileTypeString != "Split")
                        {
                            fromDirection = 0;

                            if (tile.upB == true)
                            {
                                Debug.Log("Next tile is X:" + currentTile[0] + " Y:" + currentTile[1]);
                                StartCoroutine(NextMove());
                            }
                            else
                            {
                                gm.FailedGame("NextTileSorter 159");
                            }
                        }
                        else if (tile.tileTypeString == "Split")
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = gm.tileSprites[2];
                            tile.splitIn = true;
                            tile.splitInFlow = 2;
                        }
                        else if (tile.isPump == true)
                        {
                            tile.pumpIns++;
                        }
                    }
                }
                break;
            case 3: // Left
                if (tile.name == "x:" + gm.winTile[0] + " y:" + gm.winTile[1])
                {
                    if (nextDir == gm.winDir)
                    {
                        gm.WinGame("NextTileSorter 215");
                    }
                    else
                    {
                        gm.FailedGame("NextTileSorter 219");
                    }
                }
                else
                {
                    if (tile.leftB == true)
                    {
                        currentTile[0] = currentTile[0] - 1;
                        tile = GameObject.Find("x:" + currentTile[0] + " y:" + currentTile[1]).GetComponent<TileInteraction>();

                        if (tile.isPump != true && tile.tileTypeString != "Split")
                        {
                            fromDirection = 1;

                            if (tile.rightB == true)
                            {
                                Debug.Log("Next tile is X:" + currentTile[0] + " Y:" + currentTile[1]);
                                StartCoroutine(NextMove());
                            }
                            else
                            {
                                gm.FailedGame("NextTileSorter 115");
                            }
                        }
                        else if (tile.tileTypeString == "Split")
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = gm.tileSprites[2];
                            tile.splitIn = true;
                            tile.splitInFlow = 2;
                        }
                        else if (tile.isPump == true)
                        {
                            tile.pumpIns++;
                        }
                    }
                }
                break;
        }
    }

	// Use this for initialization
	void Start () 
    {
        initialize(); 
	}

    /// <summary>
    /// Call to update starting tile
    /// </summary>
    public void initialize()
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

    /// <summary>
    /// Adds a delay so the flow is slower
    /// </summary>
    /// <returns></returns>
    IEnumerator NextMove()
    {
        yield return new WaitForSeconds(1.0f);
        NextTile();
    }
}
