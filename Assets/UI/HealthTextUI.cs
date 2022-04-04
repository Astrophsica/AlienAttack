using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthTextUI : MonoBehaviour
{
    [SerializeField]
    HealthManager healthManager;
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Health " + healthManager._health;
        healthManager._healthChanged.AddListener(UpdateHealth);
    }

    void UpdateHealth(int health)
    {
        text.text = "Health " + health;
    }
}
