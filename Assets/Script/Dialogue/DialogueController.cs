using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private float typeSpeed = 10;

    private Queue<string> paragraphs = new Queue<string>();

    private bool conversationEnded;
    private bool isTyping;

    private string p;


    public New_Movement New_Movement;


    private Coroutine typeDialogueCoroutine;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;
    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        //if there is nothing in the queue
        if (paragraphs.Count == 0) 
        { 
        if (!conversationEnded)
            {
                DisableInputs();
                //start a conversation
                StartConversation(dialogueText);
            }
            else if (conversationEnded && !isTyping)
            {
                //end the conversation
                EndConversation();
                return;
            }
        }
        //if there is something int the queue
        if (!isTyping)
        {
            p = paragraphs.Dequeue();

            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }
        
        //conversation IS being typed out
        else
        {
            FinishParagraphEarly();
        }
        

        // update conversationEnded bool
        if (paragraphs.Count == 0)
        {
            conversationEnded = true;
        }
    }
    private void StartConversation(DialogueText dialogueText)
    {
     
        //activate gameobject
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        //update the speaker name
        NPCNameText.text = dialogueText.speakerName;

        //add dialogue text to the queue
        for (int i = 0; i < dialogueText.paragraphs.Length; i++) 
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
    }
    private void EndConversation() 
    {
        EnableInputs();
        //clear the queue

        //return bool to false
        conversationEnded = false;

        //deactivate gameobject
        if (gameObject.activeSelf) 
        {
            gameObject.SetActive(false);
        
        }

    }
   /*
    private IEnumerator TypeDialogueText(string p)
    {
        float elapsedTime = 0f;

        int charIndex = 0;
        charIndex = Mathf.Clamp(charIndex, 0, p.Length);

        while (charIndex < p.Length)
        {
            elapsedTime += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(elapsedTime);
            
            NPCDialogueText.text = p.Substring(0, charIndex);

            yield return null;
        }

        NPCDialogueText.text = p;

    }
   */
   private IEnumerator TypeDialogueText(string p) 
    {
        isTyping = true;

        NPCDialogueText.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in p.ToCharArray()) 
        { 
        
        alphaIndex++;
            NPCDialogueText.text = originalText;

            displayedText = NPCDialogueText.text.Insert(alphaIndex, HTML_ALPHA);
            NPCDialogueText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        
        }

        isTyping = false;
    
    }

    private void FinishParagraphEarly()
    {
        //stop the coroutine
        StopCoroutine(typeDialogueCoroutine);

        //finish displaying text
        NPCDialogueText.text = p;

        //update isTyping bool
        isTyping = false;

    }


    public void DisableInputs()
    {
        New_Movement.canDash = false;
        New_Movement.canJump = false;
        New_Movement.canFlip = false;
        New_Movement.canMove = false;
        New_Movement.canGlide = false;
    }

    public void EnableInputs()
    {
        New_Movement.canDash = true;
        New_Movement.canJump = true;
        New_Movement.canFlip = true;
        New_Movement.canMove = true;
        New_Movement.canGlide = true;
    }


}
