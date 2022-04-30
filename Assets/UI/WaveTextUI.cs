using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Author Keiron
public class WaveTextUI : MonoBehaviour
{
    [SerializeField]
    WaveManager waveManager;
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = "Wave " + waveManager.CurrentWave.ToString();
    }
}
