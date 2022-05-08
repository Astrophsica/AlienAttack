using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author Keiron
/// Shows an AoE of turrets before placing them down
/// Effectively showing their attack range
/// </summary>
public class RangeVisualiser : MonoBehaviour
{
    float range;

    void Start()
    {
        //Area of view is Radius, * 2 turns to diameter
        range = GetComponentInParent<Shooter>().AreaOfView * 2; //Range is area of the turrets field of view
        var scaleOfParent = transform.parent.localScale.x; //Need to undo scale of parent for the child, otherwise invalid size is drawn
        range = (1.0f / scaleOfParent) * range;
        transform.localScale = new Vector3(range, range, 0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
    }

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnDisable()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

}
