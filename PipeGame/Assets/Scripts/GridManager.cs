using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Copyright Volatile Element 2014
 * 
 * Generates and manages all of the tiles in the scene
 * 
 * Has functions to change the type and rotation of the currently selected tile
 */

public class GridManager : MonoBehaviour {

    //tileSprites contains the images for all the available tile types
    //The index within this list is used througout to identify the tile type
    public List<Sprite> tileSprites;

    //marker is an overlay object that shows the user the tile they have selected
    //could probably be done with a mask of some sort?
    public GameObject marker;

    //Base tile object prefab, used to instantiate each individual tile
    public GameObject tilePrefab;

    //Used to calculate the transforms of each tile by the size of the sprites
    float spriteSize;

    //stores a reference to the currently selected tile
    GameObject activeTile;

    //References the TileInteraction script on the selected tile
    //Has all the data about the tile on it
    TileInteraction activeTileInteraction;

    public int boardSize = 10;

	void Start () {
        spriteSize = ((float)tileSprites[0].texture.width/100f);
        //Sample function call
        GenerateNewGrid(boardSize);
	}
	
	void Update () {
	
	}

    //Generates a grid of size x size
    void GenerateNewGrid(int size)
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                //Instantiates a new tile
                GameObject temp = Instantiate(tilePrefab) as GameObject;
                //Sets sprite
                temp.GetComponent<SpriteRenderer>().sprite = tileSprites[0];
                //Transforms it by the current x and y to position the tile in the grid
                Vector2 tileTransform = new Vector2(x*spriteSize, y*spriteSize);
                temp.transform.position = tileTransform;
                //Sets collider size by the sprite size
                temp.GetComponent<BoxCollider2D>().size = new Vector2(spriteSize, spriteSize);
                //Sets name and parent to clean up the editor inspectors
                temp.name = string.Format("x:{0} y:{1}", x, y);
                temp.transform.parent = gameObject.transform;
            }
        }
        //Moves the entire grid so the middle tile is at the center of the view port
        gameObject.transform.position = new Vector2(-(spriteSize * size) / 2f, -(spriteSize * size) / 2f);
    }

    //Used to change the reference to the active tile and move the marker
    public void SetActiveTile(GameObject tile)
    {
        //Sets active tile to passed tile
        activeTile = tile;
        //Checks if marker is visible, if not is set visible
        if(!marker.activeSelf)
        marker.SetActive(true);
        //Transforms the marker to the selected tiles position
        marker.transform.position = tile.transform.position;
        //Sets activeTileInteraction reference to new active tile
        activeTileInteraction = activeTile.GetComponent<TileInteraction>();
    }

    //Sets the sprite of the selected tile by the passed index
    public void SetActiveTileSprite(int index)
    {
        activeTile.GetComponent<SpriteRenderer>().sprite = tileSprites[index];
        activeTileInteraction.SetTileType(index);
    }

    //Rotates selected tile 90 degrees
    public void RotateActiveTile()
    {
        activeTileInteraction.RotateTile();
    }
}
