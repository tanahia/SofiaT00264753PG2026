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
   public void Awake()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
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
    {Debug.Log("End of tutorial.");
    }

 

}
