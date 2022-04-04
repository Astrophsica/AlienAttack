using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthTextUI : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Health " + HealthManager.Health;
        HealthManager.HealthChanged.AddListener(UpdateHealth);
    }

    void UpdateHealth(int health)
    {
        text.text = "Health " + health;
    }
}
