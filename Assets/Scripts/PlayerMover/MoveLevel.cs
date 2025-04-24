using System.Collections;
using UnityEngine;

public class MoveLevel : MonoBehaviour
{
    public GameObject door;
    private GameObject player;
    private SpriteRenderer speechBubbleRenderer;
    private Animator screenFadeAnimator;
    public string isBlackParameterName = "isBlack";

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        speechBubbleRenderer = GetComponent<SpriteRenderer>();
        screenFadeAnimator = GameObject.Find("Image").GetComponent<Animator>();
        if (screenFadeAnimator == null) 
        {
            Debug.LogError("Animator not found on Image GameObject!");
        }
        speechBubbleRenderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speechBubbleRenderer.enabled = true;

            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(TeleportWithFade());
            }
        }
    }

    private IEnumerator TeleportWithFade()
    {
        screenFadeAnimator.SetBool(isBlackParameterName, true);
        yield return new WaitForSeconds(2f);
        player.transform.position = new Vector2(door.transform.position.x, door.transform.position.y);
        screenFadeAnimator.SetBool(isBlackParameterName, false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speechBubbleRenderer.enabled = false;
        }
    }
}