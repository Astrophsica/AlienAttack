using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Author: Humza Khan
public class HealthTextUI : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        // Get text and set to health amount
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Health " + HealthManager.Health;

        // Set UpdateHealth method to be called when health changes
        HealthManager.HealthChanged.AddListener(UpdateHealth);
    }

    void UpdateHealth(int health)
    {
        // Set new health text
        text.text = "Health " + health;
    }
}
