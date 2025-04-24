using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public float delay = 30f; 
    void Start()
    {
        StartCoroutine(LoadStartMenuAfterDelay());
    }

    private IEnumerator LoadStartMenuAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("StartMenu"); 
    }
}