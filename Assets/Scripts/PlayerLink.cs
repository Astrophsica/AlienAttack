using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author: Humza Khan and Keiron Beadle
// All shop buttons link to this PlayerLink, then PlayerLink links to Player object
public class PlayerLink : MonoBehaviour
{
    [Tooltip("Reference to Player object")]
    public GameObject Player;

}
