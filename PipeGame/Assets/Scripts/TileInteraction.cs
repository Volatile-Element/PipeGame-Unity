using UnityEngine;
using System.Collections;

/*
 * Copyright Volatile Element 2014
 * 
 * Stores data about the tile such as its Type and Rotation
 * 
 * Could be used to store the input/output points for the tile
 * for example you have 1/2/3/4 refering to each side, 1 and 2 have in/out
 * store that so when the liquid flows it knows what edges should be touching
 * then when you run the water you look in here to see the rotation and where the liquid is going in and out
 * then decide if the port next to each one is connected or not?
 * 
 * Has functions to set type and rotation 
 */

public class TileInteraction : MonoBehaviour {

    //tileRotation is a number between 0 and 3 inclusively
    //0 is no rotation, 1 is 90 degrees anti-clockwise, 2 is 180 degrees and 3 is 90 degrees clockwise
    public int tileRotation;
    //tileType is the sprite index number, used to identify the tile type later 
    public int tileType;
    // 0 is defaulted top side and 1 is defaulted left side
    public int[] in_Out1 = { 0, 1 };
    // Only used if the tile has a second pipe on the tile
    public int[] in_Out2 = { 2, 3 };
    // Holding the X and Y location of this script
    public int locX, locY;
    // Allows to check if side can be moved to
    public bool upB = false;
    public bool rightB = false;
    public bool downB = false;
    public bool leftB = false;

    bool runOnce = false;

    // Only used if the tile is a pump
    public bool isPump = false;
    public int pumpOut = 2;
    public int pumpIns = 0;
    int pumpNeeds = 2; // Determins how many inputs the pump needs to activate flow

    public GameObject flowPrefab;
    
    TileInteraction up, right, down, left;
    GridManager gm;

	void Start () {
        tileRotation = 0;

        locX = int.Parse(gameObject.name.Substring(2, 1));
        locY = int.Parse(gameObject.name.Substring(6, 1));

        gm = GameObject.Find("GridManager").GetComponent<GridManager>();

        // Gets the GameObjects for the 4 directions, first checking if they are not off the board
        if (locY + 1 < gm.boardSize)
        {
            up = GameObject.Find("x:" + locX + " y:" + (locY + 1)).GetComponent<TileInteraction>();
        }

        if (locY != 0)
        {
            down = GameObject.Find("x:" + locX + " y:" + (locY - 1)).GetComponent<TileInteraction>();
        }

        if (locX + 1 < gm.boardSize)
        {
            right = GameObject.Find("x:" + (locX + 1) + " y:" + locY).GetComponent<TileInteraction>();
        }

        if (locX != 0)
        {
            left = GameObject.Find("x:" + (locX - 1) + " y:" + locY).GetComponent<TileInteraction>();
        }
	}
	

	void Update () 
    {
        if (isPump == true)
        {
            if (pumpIns == pumpNeeds)
            {
                // Initializes a new flow from the pump outwards
                // Setting the initial direction and tile
                GameObject temp = Instantiate(flowPrefab) as GameObject;
                temp.GetComponent<FlowManager>().startTile[0] = locX;
                temp.GetComponent<FlowManager>().startTile[1] = locY;

                if (pumpOut == 0 || pumpOut == 1)
                {
                    temp.GetComponent<FlowManager>().startDirection = pumpOut + 2; 
                }
                else if (pumpOut == 2)
                {
                    temp.GetComponent<FlowManager>().startDirection = 0;
                }
                else if (pumpOut == 3)
                {
                    temp.GetComponent<FlowManager>().startDirection = 1;
                }
            }
        }
         
    }

    //Rotates the tile 90 degrees on each call
    public void RotateTile()
    {
        //Sets tileRotation tracker to current angle
        if (tileRotation != 3)
        {
            tileRotation++;   
        }
        else
        {
            tileRotation = 0;
        }

        // Rotates the pump out direction only if tile is a pump
        if (isPump == true)
        {
            if (pumpOut != 3)
            {
                pumpOut++;
            }
            else
            {
                pumpOut = 0;
            }
        }

        // Rotates the in out sides
        for (int i = 0; i < in_Out1.Length; i++ )
        {
            if (in_Out1[i] != 3)
            {
                in_Out1[i]++;
            }
            else
            {
                in_Out1[i] = 0;
            }
        }

        // Rotates the in out sides
        if (in_Out2[0] != -1)
        {
            for (int i = 0; i < in_Out2.Length; i++)
            {
                if (in_Out2[i] != 3)
                {
                    in_Out2[i]++;
                }
                else
                {
                    in_Out2[i] = 0;
                }
            }
        }

        //Rotates the tile -90 degrees
        gameObject.transform.Rotate(new Vector3(0, 0, -90));

        // Updates this tiles available sides
        updateSides();

        // Updates the available sides for the tiles around this tile and only if there is actually a tile in the corresponding direction
        if (locY + 1 < gm.boardSize)
        {
            up.updateSides();
        }

        if (locX + 1 < gm.boardSize)
        {
            right.updateSides();
        }

        if (locY != 0)
        {
            down.updateSides();
        }

        if (locX != 0)
        {
            left.updateSides();
        }
    }

    //Sets tyleType to the current type index
    public void SetTileType(int index)
    {
        tileType = index;
    }

    // Updates bools to check if water can be pumped
    public void updateSides()
    {
        // Resets all sides to allow for checking to be completed carefully
        upB = false;
        rightB = false;
        downB = false;
        leftB = false;

        // Cycles through the in out ports of the current tile
        for (int i = 0; i < in_Out1.Length; i++)
        {
            switch (in_Out1[i])
            {
                case 0: // Checks tile above to availability
                    if (locY + 1 < gm.boardSize) // Makes sure there is actually a tile to the left of this tile
                    {
                        for (int upI = 0; upI < up.in_Out1.Length; upI++) // Cycles through the tile to the top's in and out ports
                        {
                            if (up.in_Out1[upI] == 2)
                            {
                                upB = true; // Sets this tile is allowed to move items upwards
                            }

                            if (up.in_Out2[upI] == 2)
                            {
                                upB = true; // Sets this tile is allowed to move items upwards
                            }
                        }
                    }
                    break;
                case 1: // Checks tile to the right to availability
                    if (locX + 1 < gm.boardSize) // Makes sure there is actually a tile to the right of this tile
                    {
                        for (int rightI = 0; rightI < right.in_Out1.Length; rightI++) // Cycles through the tile to the right's in and out ports
                        {
                            if (right.in_Out1[rightI] == 3)
                            {
                                rightB = true; // Sets this tile is allowed to move items to the right
                            }

                            if (right.in_Out2[rightI] == 3)
                            {
                                rightB = true; // Sets this tile is allowed to move items to the right
                            }
                        }
                    }
                    break;
                case 2: // Checks tile below to availability
                    if (locY != 0) // Makes sure there is actually a tile below this tile
                    {
                        for (int downI = 0; downI < down.in_Out1.Length; downI++) // Cycles through the tile to the bottom's in and out ports
                        {
                            if (down.in_Out1[downI] == 0)
                            {
                                downB = true; // Sets this tile is allowed to move items downwards
                            }

                            if (down.in_Out2[downI] == 0)
                            {
                                downB = true; // Sets this tile is allowed to move items downwards
                            }
                        }
                    }
                    break;
                case 3: // Checks tile to the left to availability
                    if (locX != 0) // Makes sure there is actually a tile to the left of this tile
                    {
                        for (int leftI = 0; leftI < left.in_Out1.Length; leftI++) // Cycles through the tile to the left's in and out ports
                        {
                            if (left.in_Out1[leftI] == 1)
                            {
                                leftB = true; // Sets this tile is allowed to move items to the left
                            }

                            if (left.in_Out2[leftI] == 1)
                            {
                                leftB = true; // Sets this tile is allowed to move items to the left
                            }
                        }
                    }
                    break;
            }
        }


        // Cycles through the in out ports of the current tile
        if (in_Out2[0] != -1) // Only runs when there is a second pipe on the tile
        {
            for (int i = 0; i < in_Out2.Length; i++)
            {
                switch (in_Out2[i])
                {
                    case 0: // Checks tile above to availability
                        if (locY + 1 < gm.boardSize) // Makes sure there is actually a tile to the left of this tile
                        {
                            for (int upI = 0; upI < up.in_Out2.Length; upI++) // Cycles through the tile to the top's in and out ports
                            {
                                if (up.in_Out1[upI] == 2)
                                {
                                    upB = true; // Sets this tile is allowed to move items upwards
                                }

                                if (up.in_Out2[upI] == 2)
                                {
                                    upB = true; // Sets this tile is allowed to move items upwards
                                }
                            }
                        }
                        break;
                    case 1: // Checks tile to the right to availability
                        if (locX + 1 < gm.boardSize) // Makes sure there is actually a tile to the right of this tile
                        {
                            for (int rightI = 0; rightI < right.in_Out2.Length; rightI++) // Cycles through the tile to the right's in and out ports
                            {
                                if (right.in_Out1[rightI] == 3)
                                {
                                    rightB = true; // Sets this tile is allowed to move items to the right
                                }

                                if (right.in_Out2[rightI] == 3)
                                {
                                    rightB = true; // Sets this tile is allowed to move items to the right
                                }
                            }
                        }
                        break;
                    case 2: // Checks tile below to availability
                        if (locY != 0) // Makes sure there is actually a tile below this tile
                        {
                            for (int downI = 0; downI < down.in_Out2.Length; downI++) // Cycles through the tile to the bottom's in and out ports
                            {
                                if (down.in_Out1[downI] == 0)
                                {
                                    downB = true; // Sets this tile is allowed to move items downwards
                                }

                                if (down.in_Out2[downI] == 0)
                                {
                                    downB = true; // Sets this tile is allowed to move items downwards
                                }
                            }
                        }
                        break;
                    case 3: // Checks tile to the left to availability
                        if (locX != 0) // Makes sure there is actually a tile to the left of this tile
                        {
                            for (int leftI = 0; leftI < left.in_Out2.Length; leftI++) // Cycles through the tile to the left's in and out ports
                            {
                                if (left.in_Out1[leftI] == 1)
                                {
                                    leftB = true; // Sets this tile is allowed to move items to the left
                                }

                                if (left.in_Out2[leftI] == 1)
                                {
                                    leftB = true; // Sets this tile is allowed to move items to the left
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
