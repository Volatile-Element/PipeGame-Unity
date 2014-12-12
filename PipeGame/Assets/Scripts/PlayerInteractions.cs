using UnityEngine;
using System.Collections;

/*
 * Copyright Volatile Element 2014
 * 
 * Handles all player interaction within the game
 * 
 * Put all click, touch or key actions in here
 */
public class PlayerInteractions : MonoBehaviour {

    GridManager gridManager;

	void Start () {
        gridManager = FindObjectOfType<GridManager>();
	}
	
	void Update () {
        //Mouse actions, used for tile interaction
        if (Input.GetMouseButtonDown(0))
        {
            ClickActions();
        }

        //Place holder actions for changing tile type and rotation
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangeTile(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeTile(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeTile(2);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateTile();
        }
	
	}

    //Sets the active tile by Raycasting down
    void ClickActions()
    {
        //Gets the collision between a physics ray and objects in the scene
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        //Only trys to change the active tile if the ray actually hit something
        if(hit)
        gridManager.SetActiveTile(hit.collider.gameObject);
    }

    //Changes the active tiles tile type, works on an index system, 0 is blank space
    void ChangeTile(int index)
    {
        gridManager.SetActiveTileSprite(index);
    }

    //Rotates the active tile by 90 degrees for each press
    void RotateTile()
    {
        gridManager.RotateActiveTile();
    }
}
