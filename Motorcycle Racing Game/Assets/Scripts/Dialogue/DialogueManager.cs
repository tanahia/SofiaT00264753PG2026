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
   
    public TextMeshProUGUI dialogueText;
    public Animator animator;
   public void Awake()
    {
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
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("End of tutorial.");
    }

 

}
