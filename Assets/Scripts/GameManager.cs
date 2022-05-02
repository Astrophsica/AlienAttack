using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author Keiron
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    GameObject Player;
    [SerializeField]
    WaveManager WaveManager;
    [SerializeField]
    Button NextWaveButton; 
    bool _buildMode = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public bool InBuildMode()
    {
        return _buildMode;
    }

    public void SwitchState()
    {
        _buildMode = !_buildMode;
        if (_buildMode)
            SetupBuildMode();
        else
            SetupPlayMode();
    }

    private void SetupPlayMode()
    {
        WaveManager.StartWave();
        Player.GetComponent<Placer>().enabled = false;
        NextWaveButton.GetComponent<RectTransform>().position -= new Vector3(0,100,0);
        NextWaveButton.enabled = false;
    }

    private void SetupBuildMode()
    {
        Player.GetComponent<Placer>().enabled = true;
        NextWaveButton.GetComponent<RectTransform>().position += new Vector3(0, 100, 0);
        NextWaveButton.enabled = true;
    }
}
