using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author Keiron
public class MenuButtonHandler : MonoBehaviour
{
    public void QuitButton()
    {
        Application.Quit(0);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
}
