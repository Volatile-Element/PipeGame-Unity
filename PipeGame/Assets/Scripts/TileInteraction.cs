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

	void Start () {
        tileRotation = 0;
	}
	

	void Update () {
	
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
        //Rotates the tile 90 degrees
        gameObject.transform.Rotate(new Vector3(0, 0, 90));
    }

    //Sets tyleType to the current type index
    public void SetTileType(int index)
    {
        tileType = index;
    }
}
