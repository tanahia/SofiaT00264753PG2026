using System;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue tutorial;
    public Dialogue choiceMaking;
    MotorcycleMovement player;
    MotorcycleMovement.State currentState;
    [SerializeField] TextMeshProUGUI hideChoiceDialogue;
    bool isTutorial;

    public void Start()
    {
        hideChoiceDialogue.gameObject.SetActive(false);
        player = FindFirstObjectByType<MotorcycleMovement>();
        TriggerTutorial();
    }
    public void Update()
    {
        currentState = player.getCurrentState();
        if(currentState == MotorcycleMovement.State.IntersectionChoice)
            {
            hideChoiceDialogue.gameObject.SetActive(true);
            TriggerChoiceMaking();
        }

    }
    public void TriggerTutorial ()
    {
        isTutorial=true;
       
        FindFirstObjectByType<DialogueManager>().StartDialogue(tutorial);
        
    }
    public void TriggerChoiceMaking()
    {
        isTutorial=false;
        FindFirstObjectByType<DialogueManager>().StartDialogue(choiceMaking);
    }

    public void DialogEnded()
    {
       player.GoToState(MotorcycleMovement.State.UserControled);                              
    }
    public bool getToggle()
    {
        return isTutorial;
    }
}
