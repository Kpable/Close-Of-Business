using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// Main Menu Controller
/// 
/// Created By: Kpable
/// Date Created: 08-06-17
/// 
/// </summary>
public class MainMenuController : MonoBehaviour {

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenWebsite()
    {
        Application.OpenURL("http://unstable-studios.com/");
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
