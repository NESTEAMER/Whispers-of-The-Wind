using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Loaded Game Successfully");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Exit");
    }
}