using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public GameObject ObjectToPlace;
    LayerMask _backgroundLayer;
    LayerMask _wallLayer;

    void Start()
    {
        _backgroundLayer = ~LayerMask.NameToLayer("Background");
        _wallLayer = LayerMask.GetMask("Walls");
    }

    void Update()
    {
        //raycast mouse position to get world coordinates
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 5.0f, _backgroundLayer);
        //if left clicked, place wall at center of grid box closest to mouse position
        if (Input.GetMouseButtonDown(0)){
            Vector2 spawnPoint = GetClosestCentrePointToHit(hit.point);

            if (WallExistsAtPoint(spawnPoint)) //Check if wall already at point
            {
                return; 
            }
            Instantiate(ObjectToPlace, spawnPoint, Quaternion.identity);
            AstarPath.active.Scan(); //Rescans the grid to adjust for new block
            //This isn't needed when there is a "Build" and "Action" phase to the game, but for now.
        }
    }

    private bool WallExistsAtPoint(Vector2 point)
    {
        if (Physics2D.Raycast(point, Vector2.zero, 5.0f, _wallLayer).collider != null)
        {
            Debug.LogWarning("Wall already at position: " + point);
            return true;
        }
        return false;
    }

    private Vector2 GetClosestCentrePointToHit(Vector2 point)
    {
        Vector2 centrePoint = new Vector2();
        //we want a coordinate that's X.5 and Y.5
        double xRounded = Math.Round(point.x);
        double yRounded = Math.Round(point.y);
        //take x = 4.4 , y = 2.9 as example,
        // 4.4 - 4.0 = 0.4, positive [[]] 2.9 - 3.0 = -0.1, negative, we now know if closest centre based on this
        centrePoint.x = (point.x - xRounded < 0) ? (float)xRounded-0.5f : (float)xRounded + 0.5f;
        centrePoint.y = (point.y - yRounded < 0) ? (float)yRounded-0.5f : (float)yRounded + 0.5f;
        return centrePoint;
    }
}
