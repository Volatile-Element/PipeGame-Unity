using UnityEngine;
using System.Collections;

/*
 * Copyright Volatile Element 2014
 * 
 * Put all click, touch or key actions in here
 * 
 * Methods:
 * Start()
 * Update()
 * ClickActions()
 * ChangeTile(int index)
 * RotateTile()
 * 
 * Coders:
 * Ashley Blake-Hood (Creator)
 * Matthew Moore (Editor)
 * 
 */

/// <summary>
/// Handles all player interaction within the game
/// </summary>
public class PlayerInteractions : MonoBehaviour {

    GridManager gridManager;
    FlowManager flowManager;

	void Start () {
        gridManager = FindObjectOfType<GridManager>();
        flowManager = FindObjectOfType<FlowManager>();
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            flowManager.StartFlow();
        }
	
	}

    /// <summary>
    /// Sets the active tile by Raycasting down
    /// </summary>
    void ClickActions()
    {
        //Gets the collision between a physics ray and objects in the scene
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        //Only trys to change the active tile if the ray actually hit something
        if(hit)
        gridManager.SetActiveTile(hit.collider.gameObject);
    }

    /// <summary>
    /// Changes the active tiles tile type, works on an index system, 0 is blank space
    /// </summary>
    /// <param name="index"></param>
    void ChangeTile(int index)
    {
        gridManager.SetActiveTileSprite(index);
    }

    /// <summary>
    /// Rotates the active tile by 90 degrees for each press
    /// </summary>
    void RotateTile()
    {
        gridManager.RotateActiveTile();
    }
}
