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
    [SerializeField] TextMeshProUGUI hideTutorial;
    bool isTutorial;

    public void Start()
    {
        hideChoiceDialogue.gameObject.SetActive(false);
        hideTutorial.gameObject.SetActive(true);
        player = FindFirstObjectByType<MotorcycleMovement>();
        TriggerTutorial();
    }
    public void Update()
    {
        currentState = player.getCurrentState();
        if(currentState == MotorcycleMovement.State.IntersectionChoice)
            {
            hideChoiceDialogue.gameObject.SetActive(true);
            hideTutorial.gameObject.SetActive(false);
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
