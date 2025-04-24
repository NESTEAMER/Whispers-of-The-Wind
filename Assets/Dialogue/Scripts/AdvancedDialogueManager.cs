using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedDialogueManager : MonoBehaviour
{

    //NPC dialogue currently stepping through
    private AdvancedDialogueSO currentConversation;
    private int stepNum;
    private bool dialogueActivated;

    //UI References
    private GameObject dialogueCanvas;
    private TMP_Text actor;
    private Image portrait;
    private TMP_Text dialogueText;

    private string currentSpeaker;
    private Sprite currentPortrait;

    public ActorSO[] actorSO;


    //button reffs
    private GameObject[] optionButton;
    private TMP_Text[] optionButtonText;
    private GameObject optionsPanel;

    //typewriter effect
    [SerializeField]
    private float typingSpeed = 0.02f;
    private Coroutine typeWriterRoutine;
    private bool canContinueText = true;


    // Start is called before the first frame update
    void Start()
    {
        //find buttons
        optionButton = GameObject.FindGameObjectsWithTag("OptionButton");
        optionsPanel = GameObject.Find("OptionsPanel");
        optionsPanel.SetActive(false);

        //find tmpt text on buttons
        optionButtonText = new TMP_Text[optionButton.Length];
        for(int i = 0; i < optionButton.Length; i++)
        {
            optionButtonText[i] = optionButton[i].GetComponentInChildren<TMP_Text>();
        }

        //turn off buttons to start
        for (int i = 0; i < optionButton.Length; i++)
        {
            optionButton[i].SetActive(false);
        }

        dialogueCanvas = GameObject.Find("DialogueCanvas");
        actor = GameObject.Find("ActorText").GetComponent<TMP_Text>();
        portrait = GameObject.Find("Portrait").GetComponent<Image>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<TMP_Text>();

        dialogueCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueActivated && Input.GetButtonDown("Interact") && canContinueText)
        {
            //cancel dialogue if there are no lines of dialogue remaining
            if (stepNum >= currentConversation.actors.Length)
                TurnOffDialogue();

            //continue dialogue
            else
            {
                PlayDialogue();
            }
        }
    }

    void PlayDialogue()
    {
        //if random NPC
        if(currentConversation.actors[stepNum] == DialogueActors.Random)
        {
            SetActorInfo(false);
        }
        //if recurring Character
        else
        {
            SetActorInfo(true);
        }

        //Display Dialogue
        actor.text = currentSpeaker;
        portrait.sprite = currentPortrait;

        //if there is a branch
        if(currentConversation.actors[stepNum] == DialogueActors.Branch)
        {
            for (int i = 0; i < currentConversation.optionText.Length; i++)
            {
                if(currentConversation.optionText[i] == null)
                {
                    optionButton[i].SetActive(false);
                }
                else
                {
                    optionButtonText[i].text = currentConversation.optionText[i];
                    optionButton[i].SetActive(true);
                }

                //set the first button to be auto-selected
                optionButton[0].GetComponent<Button>().Select();
            }
        }

        //keep routine from running multiple times at once
        if(typeWriterRoutine != null)
        {
            StopCoroutine(typeWriterRoutine);
        }
        if(stepNum < currentConversation.dialogue.Length)
        {
            typeWriterRoutine = StartCoroutine(TypeWriterEffect(dialogueText.text = currentConversation.dialogue[stepNum]));
        }
        else
        {
            optionsPanel.SetActive(true);
        }
        
        dialogueCanvas.SetActive(true);
        stepNum += 1;
    }

    void SetActorInfo(bool recurringCharacter)
    {
        if(recurringCharacter)
        {
            for(int i = 0;i < actorSO.Length; i++)
            {
                if(actorSO[i].name == currentConversation.actors[stepNum].ToString())
                {
                    currentSpeaker = actorSO[i].actorName;
                    currentPortrait = actorSO[i].actorPortrait;
                }
            }
        }
        else
         {
          currentSpeaker = currentConversation.randomActorName;
          currentPortrait = currentConversation.randomActorPortrait;
         }
    }

    public void InitiateDialogue(NPCDialogue npcDialogue)
    {
        //array of convo for the current npc
        currentConversation = npcDialogue.conversation[0];

        //enable convo box
        dialogueActivated = true;
        

        //console check debugger
        Debug.Log("Started conversation: " + currentConversation);
    }

    public void Option(int optionNum)
    {
        foreach (GameObject button in optionButton)
        {
            button.SetActive(false);
        }

        if(optionNum == 0)
        {
            currentConversation = currentConversation.option0;
        }
        if(optionNum == 1)
        {
            currentConversation = currentConversation.option1;
        }
        if(optionNum == 2)
        {
            currentConversation = currentConversation.option2;
        }
        if(optionNum == 3)
        {
            currentConversation = currentConversation.option3;
        }

        stepNum = 0;
    }

    private IEnumerator TypeWriterEffect(string line)
    {
        dialogueText.text = "";
        canContinueText = false;
        yield return new WaitForSeconds(.5f);
        foreach(char letter in line.ToCharArray())
        {
            if(Input.GetButtonDown("Interact"))
            {
                dialogueText.text = line;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        canContinueText = true;
    }

    public void TurnOffDialogue()
    {
        stepNum = 0;

        //disable convo box
        dialogueActivated = false;
        optionsPanel.SetActive(false);
        dialogueCanvas.SetActive(false);

        //console check debugger
        Debug.Log("Ended conversation. Reset the step to " + stepNum);
    }
}

public enum DialogueActors //remember to drag new actors to the DialogueManager object
{
    Player,
    Kai,
    Random,
    Branch,
    Arma,
    Gusjarot
};
