using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    //This static variable is changed by the UI scripts
    private GameObject _objectToPlace = null;
    public GameObject ObjectToPlace { 
        get { return _objectToPlace; }
        set 
        { 
            _objectToPlace = value;
            SetGhostNull();
        }
    }
    private int _objectLayer = 0;
    private GameObject _objectGhost = null;
    private SpriteRenderer _objectGhostSprite = null;
    private Color _invalidPlaceColor = new Color(1, 0, 0, 0.65f);
    LayerMask _backgroundLayer;
    LayerMask _structureLayer;

    void Start()
    {
        _structureLayer = LayerMask.GetMask("Wall", "Turret", "Stronghold");
        _backgroundLayer = ~LayerMask.NameToLayer("Background");
    }
    void Update()
    {
        if (ObjectToPlace == null) { return; }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 5.0f, _backgroundLayer);
        if (hit.collider == null) 
        {
            //Need to make sure mouse is over the game, not the UI
            //Still deciding on this, but for now, delete object to place when go outside of screen
            if (_objectGhost != null)
            {
                Destroy(_objectGhost.gameObject);
                SetGhostNull();
            }
            return; 
        }
        if (_objectGhost == null)
        {
            Vector2 spawnPoint = GetClosestCentrePointToHit(hit.point);
            _objectGhost = Instantiate(ObjectToPlace, spawnPoint, Quaternion.identity);
            TurnObjectToGhost();
        }
        else {
            Vector2 tempSpawn = GetClosestCentrePointToHit(hit.point);
            Vector3 newPosition = new Vector3(tempSpawn.x, tempSpawn.y,1.0f);
            _objectGhost.transform.position = newPosition;     
        }

        bool canPlace = !ObjectAtPoint(_objectGhost.transform.position);

        if (!canPlace)
            _objectGhostSprite.color = _invalidPlaceColor;
        else
            _objectGhostSprite.color = Color.white;

        if (canPlace && Input.GetMouseButtonDown(0))
        {
            //ObjectGhost.transform.position -= Vector3.forward;
            TurnGhostToObject();
            SetGhostNull();
            AstarPath.active.Scan(); //Rescans the grid to adjust for new block
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(_objectGhost.gameObject); //User cancels placement
            SetGhostNull();
            ObjectToPlace = null;
        }
    }

    private void TurnGhostToObject()
    {
        _objectGhost.GetComponent<BoxCollider2D>().isTrigger = false;
        _objectGhost.layer = _objectLayer;
        _objectLayer = 0;
    }

    private void TurnObjectToGhost()
    {
        _objectGhost.GetComponent<BoxCollider2D>().isTrigger = true;
        _objectLayer = _objectGhost.layer;
        _objectGhost.layer = 0;
        _objectGhostSprite = _objectGhost.GetComponent<SpriteRenderer>();
    }

    private bool ObjectAtPoint(Vector2 point)
    {
        if (Physics2D.Raycast(point, Vector2.zero, 5.0f, _structureLayer).collider != null)
        {
            //Debug.LogWarning("Structure already at position: " + point);
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

    void SetGhostNull()
    {
        _objectGhost = null;
        _objectGhostSprite = null;
    }
}
