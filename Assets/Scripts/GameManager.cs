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
    GameObject AudioManager;
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
        var placer = Player.GetComponent<Placer>();
        placer.enabled = false;
        placer.DeleteHeldObject();
        NextWaveButton.GetComponent<RectTransform>().position -= new Vector3(0,100,0);
        NextWaveButton.enabled = false;
        var audioSources = AudioManager.GetComponents<AudioSource>();
        audioSources[0].Stop();
        audioSources[1].Play();
    }

    private void SetupBuildMode()
    {
        var placer = Player.GetComponent<Placer>();
        placer.enabled = true;
        NextWaveButton.GetComponent<RectTransform>().position += new Vector3(0, 100, 0);
        NextWaveButton.enabled = true;
        var audioSources = AudioManager.GetComponents<AudioSource>();
        audioSources[1].Stop();
        audioSources[0].Play();
    }
}
