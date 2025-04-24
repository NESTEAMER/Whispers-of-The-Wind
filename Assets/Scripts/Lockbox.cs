using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class Lockbox : MonoBehaviour
{
    [SerializeField]
    GameObject codePanel;
    [SerializeField]
    TMP_Text codeText;     string codeTextValue = "";

    private SpriteRenderer speechBubbleRenderer;

    void Start()
    {
        codePanel.SetActive(false);
        speechBubbleRenderer = GetComponent<SpriteRenderer>();
        speechBubbleRenderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speechBubbleRenderer.enabled = true;

            if (Input.GetKey(KeyCode.E))
            {
                codePanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speechBubbleRenderer.enabled = false;
            codePanel.SetActive(false);
            codeTextValue = "";
        }
    }

    void Update()
    {
        codeText.text = codeTextValue;

        if (codeTextValue == "1987")
        {
            Debug.Log("Correct code!");
            SceneManager.LoadScene("Good Ending");
            codePanel.SetActive(false);
        }

        if (codeTextValue.Length > 4)
        {
            codeTextValue = "";
        }
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }
}