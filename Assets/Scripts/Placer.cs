using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Author: Keiron Beadle (Main) and Humza Khan (Improvements and Money system)
public class Placer : MonoBehaviour
{
    private GameObject _objectToPlace = null; //Game Object of item user has selected
    public ShopItem SelectedShopItem = null; //Item user has selected
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
    private Color _invalidPlaceColor = new Color(1, 0, 0, 0.65f); //Semi-transparent red colour
    LayerMask _backgroundLayer;
    LayerMask _structureLayer;

    void Start()
    {
        _structureLayer = LayerMask.GetMask("Wall", "Turret", "Stronghold");
        _backgroundLayer = ~LayerMask.NameToLayer("Background");
    }

    void Update()
    {
        if (SelectedShopItem == null || ObjectToPlace == null) { return; } //Nothing selected? Return
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 5.0f, _backgroundLayer); 
        // ^^ Finds world position of user's mouse, i.e. a coordinate to place the ObjectToPlace
        if (hit.collider == null) //Collider is null if raycast does not meet a collision box/sphere i.e. it's outside the map or in the shop
        {
            if (_objectGhost != null)
            {
                //If the user cursor is out of map, despawn the ObjectToPlace
                Destroy(_objectGhost.gameObject); 
                SetGhostNull();
            }
            return; 
        }
        if (_objectGhost == null)
        {
            //If user cursor in map, but no object yet, instantiate a new GameObject of ObjectToPlace
            Vector2 spawnPoint = GetClosestCentrePointToHit(hit.point);
            _objectGhost = Instantiate(ObjectToPlace, spawnPoint, Quaternion.identity);
            TurnObjectToGhost();
        }
        else {
            //Otherwise, don't spawn a new one - instead just move the current ObjectToPlace to the user cursor
            Vector2 tempSpawn = GetClosestCentrePointToHit(hit.point);
            Vector3 newPosition = new Vector3(tempSpawn.x, tempSpawn.y,1.0f);
            _objectGhost.transform.position = newPosition;     
        }

        bool hasMoney = ShopManager.Money - SelectedShopItem.Price >= 0; //Valid user amount of money to buy tower
        bool overUI = IsMouseOverUI(); //User cursor is over UI
        bool canPlace = hasMoney && !overUI && !ObjectAtPoint(_objectGhost.transform.position); //Can user can place the tower 
        _objectGhostSprite.color = canPlace ? Color.white : _invalidPlaceColor; //Selects colour of RangeVisualiser based on canPlace

        if (canPlace && Input.GetMouseButtonDown(0)) //If left clicked and user can place tower
        {
            //Turn the semi-transparent object to a real object, e.g. disable it moving with the user cursor
            TurnGhostToObject();
            if (AStarManager.Instance.IsCutoff()) //If the object is now cutting off the A* Pathfinder to the goal destination
            {
                TurnObjectToGhost(); //Don't place down the tower, and give it an invalid colour
                _objectGhostSprite.color = _invalidPlaceColor;
                AstarPath.active.Scan(); //Rescans the grid to adjust for new block
                return;
            }
            SetGhostNull();
            ShopManager.SpendMoney(SelectedShopItem.Price); //Removes money from player's wallet / bank
            AstarPath.active.Scan(); //Rescans the grid to adjust for new block
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeleteHeldObject(); //If right clicked, delete the selected object
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void TurnGhostToObject()
    {
        //Trigger false means A* Pathfinding can now 'see' it, and therefore check for invalid placement
        _objectGhost.GetComponent<BoxCollider2D>().isTrigger = false;
        Shooter shooter;
        if ((shooter = _objectGhost.GetComponent<Shooter>()))
        {
            shooter.enabled = true;
            _objectGhost.GetComponentInChildren<RangeVisualiser>().enabled = false;
        }
        _objectGhost.layer = _objectLayer;
        _objectLayer = 0;
    }

    private void TurnObjectToGhost()
    {
        //Trigger true means A* Pathfinding perceives the object as invisible
        _objectGhost.GetComponent<BoxCollider2D>().isTrigger = true;
        Shooter shooter;
        if ((shooter = _objectGhost.GetComponent<Shooter>())){
            shooter.enabled = false;
            _objectGhost.GetComponentInChildren<RangeVisualiser>().enabled = true;
        }
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

    private Vector2 ConvertNodePositionToHalfPoint(Vector2 position)
    {
        //A* Nodes can be tricky to work with
        //This code handles converting the node positions
        //To World positions
        float x = position.x;
        float y = position.y;
        float lowerX = Mathf.Floor(x) - 0.5f;
        float higherX = Mathf.Ceil(x) - 0.5f;
        float lowerY = Mathf.Floor(y) - 0.5f;
        float higherY = Mathf.Ceil(y) - 0.5f;
        x = (x-lowerX) < (x-higherX) ? lowerX : higherX;
        y = (y - lowerY) < (y - higherY) ? lowerY : higherY;
        return new Vector2(x, y);
    }

    /// <summary>
    /// Returns the closest grid aligned position to the point given
    /// </summary>
    /// <returns></returns>
    private Vector2 GetClosestCentrePointToHit(Vector2 point)
    {
        var nearestNode = AstarPath.active.GetNearest(point);
        return ConvertNodePositionToHalfPoint(nearestNode.position);
    }

    public void DeleteHeldObject()
    {
        if (_objectGhost == null) { return; }
        Destroy(_objectGhost.gameObject); //User cancels placement
        SetGhostNull();
        ObjectToPlace = null;
        SelectedShopItem = null;
    }

    void SetGhostNull()
    {
        _objectGhost = null;
        _objectGhostSprite = null;
    }
}
