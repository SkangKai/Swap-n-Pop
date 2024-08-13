using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string level1;

    public void PlayGame()
    {
        SceneManager.LoadScene(level1);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
