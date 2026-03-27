using GLTF.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue <string> sentences;//First in First out
   
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] TextMeshProUGUI dialogueIntersectionText;
    [SerializeField] TextMeshProUGUI dialogueTIntersectionLeftText;
    [SerializeField] TextMeshProUGUI dialogueTIntersectionRightText;
    [SerializeField] Animator animator;
    [SerializeField] Animator rightTurn;
    DialogueTrigger.State currentState;
    MotorcycleMovement player;
    DialogueTrigger manager;
    public void Awake()
    {
        manager = FindFirstObjectByType<DialogueTrigger>();
        player = FindFirstObjectByType<MotorcycleMovement>();

        sentences = new Queue<string>();
       
    }
    public void StartDialogue(Dialogue dialogue)
    {
        currentState = manager.getCurrentDialogueState();
        animator.SetBool("isOpen", true);

         sentences.Clear();

           foreach (string sentence in dialogue.sentences)
           {
               sentences.Enqueue(sentence);
           }
           DisplayNextSentence();
       
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
           
            EndDialogue();
            return;
        }
       
        string sentence = sentences.Dequeue();

        if (currentState==DialogueTrigger.State.Tutorial)
            tutorialText.text = sentence;
        else if(currentState == DialogueTrigger.State.IntersectionDialogue)
            dialogueIntersectionText.text = sentence;
        else if (currentState == DialogueTrigger.State.TIntersectionLeft)
            dialogueTIntersectionLeftText.text = sentence;
        else if (currentState == DialogueTrigger.State.TIntersectionRight)
            dialogueTIntersectionRightText.text = sentence;
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        manager.DialogEnded();
        Debug.Log("End of tutorial.");
        
    }

    public void turnRight()
    {

        player.StartCoroutine(player.Turn(90f));
    }
    public void turnLeft()
    {
        player.StartCoroutine(player.Turn(-90f));
    }

}
