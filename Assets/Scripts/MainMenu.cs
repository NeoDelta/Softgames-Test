using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnStart(LevelSO level)
    {
        LoadSceneManager.Instance.LoadLevel(level);
    }

    public void OnQuitApp()
    {
        Application.Quit();
    }
}
