using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeVisualiser : MonoBehaviour
{

    float range;

    void Start()
    {
        range = GetComponentInParent<Shooter>().AreaOfView;
        transform.localScale = new Vector3(range, range, 0.1f);
    }

    void Update()
    {
        transform.localScale = new Vector3(range, range, 0.1f);

    }
}
