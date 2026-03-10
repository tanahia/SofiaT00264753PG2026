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
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Animator animator;
    DialogueTrigger toggle;
    bool isTutorial;


    DialogueTrigger manager;
    public void Awake()
    {
        manager = FindFirstObjectByType<DialogueTrigger>();
        toggle = FindFirstObjectByType<DialogueTrigger>();
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        Debug.Log("Starting tutorial with " + dialogue.sentences.Length + " sentences.");

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
         isTutorial = toggle.getToggle();
        if (isTutorial)
            tutorialText.text = sentence;
        else
            dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        manager.DialogEnded();
        Debug.Log("End of tutorial.");
    }

 

}
