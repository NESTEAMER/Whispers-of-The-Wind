using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{

    public AdvancedDialogueSO[] conversation;

    private Transform player;
    private SpriteRenderer speechBubbleRenderer;

    private AdvancedDialogueManager advancedDialogueManager;

    private bool dialogueInitiated;

    // Start is called before the first frame update
    void Start()
    {
        advancedDialogueManager = GameObject.Find("DialogueManager").GetComponent<AdvancedDialogueManager>();
        speechBubbleRenderer = GetComponent<SpriteRenderer>();
        speechBubbleRenderer.enabled = false;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !dialogueInitiated)
        {
            //speech bubble on
            speechBubbleRenderer.enabled = true;

            //find player's transform
            player = collision.gameObject.GetComponent<Transform>();

            //check where player is and look at player
            if(player.position.x > transform.position.x && transform.parent.localScale.x < 0)
            {
                Flip();
            }
            else if (player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }

            advancedDialogueManager.InitiateDialogue(this);
            dialogueInitiated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //speech bubble off
            speechBubbleRenderer.enabled = false;

            advancedDialogueManager.TurnOffDialogue();
            dialogueInitiated = false;
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1;
        transform.parent.localScale = currentScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
