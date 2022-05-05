using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Author: Keiron Beadle (Main) and Humza Khan (Improvements and Money system)
public class Placer : MonoBehaviour
{
    //This static variable is changed by the UI scripts
    private GameObject _objectToPlace = null;
    public ShopItem SelectedShopItem = null;
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
        if (SelectedShopItem == null || ObjectToPlace == null) { return; }
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
            Debug.Log(newPosition);
            _objectGhost.transform.position = newPosition;     
        }

        bool hasMoney = ShopManager.Money - SelectedShopItem.Price >= 0;
        bool overUI = IsMouseOverUI();
        bool canPlace = hasMoney && !overUI && !ObjectAtPoint(_objectGhost.transform.position);
        _objectGhostSprite.color = canPlace ? Color.white : _invalidPlaceColor;

        if (canPlace && Input.GetMouseButtonDown(0))
        {
            TurnGhostToObject();
            if (AStarManager.Instance.IsCutoff())
            {
                TurnObjectToGhost();
                _objectGhostSprite.color = _invalidPlaceColor;
                return;
            }
            SetGhostNull();
            ShopManager.SpendMoney(SelectedShopItem.Price);
            AstarPath.active.Scan(); //Rescans the grid to adjust for new block
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeleteHeldObject();
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void TurnGhostToObject()
    {
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

    private Vector2 GetClosestCentrePointToHit(Vector2 point)
    {
        var nearestNode = AstarPath.active.GetNearest(point);
        return ConvertNodePositionToHalfPoint(nearestNode.position);
    }

    public void DeleteHeldObject()
    {
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
