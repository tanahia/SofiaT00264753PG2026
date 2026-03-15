using System;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue tutorial;
    public Dialogue choiceMaking;
    MotorcycleMovement player;
    MotorcycleMovement.State currentState;

    [SerializeField] TextMeshProUGUI hideIntersectionDialogue;
    [SerializeField] TextMeshProUGUI hideTutorial;
    [SerializeField] TextMeshProUGUI hideTIntersectionLeftDialogue;
    [SerializeField] TextMeshProUGUI hideTIntersectionRightDialogue;
    [SerializeField] TextMeshProUGUI hideEnd;
    [SerializeField] TextMeshProUGUI hideTIntersectionDialogueRightLeft;
    bool dialogueStarted = false;
    public enum State
    {
        Tutorial,
        IntersectionDialogue,
        TIntersectionLeft,
        TIntersectionRight,
        CornerDialogue,
        EndDialogue,
        TIntersectionDialogueRightLeft
    }
    State currentDialogueState=State.Tutorial;
    public void Start()
    {     
        player = FindFirstObjectByType<MotorcycleMovement>();
        hideIntersectionDialogue.gameObject.SetActive(false);
        hideTIntersectionLeftDialogue.gameObject.SetActive(false);
        hideTIntersectionRightDialogue.gameObject.SetActive(false);
        hideEnd.gameObject.SetActive(false);
    }
    public void Update()
    {
        currentState = player.getCurrentState();
        if (currentState == MotorcycleMovement.State.Dialogue && !dialogueStarted)
        {
            dialogueStarted = true;
            switch (currentDialogueState)
            {

                case State.Tutorial:
                    hideTutorial.gameObject.SetActive(true);
                    TriggerTutorial();
                    break;
                case State.IntersectionDialogue:
                    hideIntersectionDialogue.gameObject.SetActive(true);
                    hideTIntersectionLeftDialogue.gameObject.SetActive(false);
                    hideTIntersectionRightDialogue.gameObject.SetActive(false);
                    hideTutorial.gameObject.SetActive(false);
                    hideEnd.gameObject.SetActive(false);
                    TriggerChoiceMaking();
                    break;
                case State.TIntersectionLeft:
                    hideTIntersectionLeftDialogue.gameObject.SetActive(true);
                    hideTIntersectionRightDialogue.gameObject.SetActive(false);
                    hideIntersectionDialogue.gameObject.SetActive(false);
                    hideTutorial.gameObject.SetActive(false);
                    hideEnd.gameObject.SetActive(false);
                    TriggerChoiceMaking();
                    Debug.Log("T intersection left");
                    break;
                case State.TIntersectionRight:
                    hideTIntersectionLeftDialogue.gameObject.SetActive(false);
                    hideTIntersectionRightDialogue.gameObject.SetActive(true);
                    hideIntersectionDialogue.gameObject.SetActive(false);
                    hideTutorial.gameObject.SetActive(false);
                    hideEnd.gameObject.SetActive(false);
                    TriggerChoiceMaking();
                    Debug.Log("T intersection right");
                    break;
                case State.CornerDialogue:

                    Debug.Log("Corner");
                    break;
                case State.EndDialogue:
                    hideTIntersectionLeftDialogue.gameObject.SetActive(false);
                    hideTIntersectionRightDialogue.gameObject.SetActive(false);
                    hideIntersectionDialogue.gameObject.SetActive(false);
                    hideTutorial.gameObject.SetActive(false);
                    hideEnd.gameObject.SetActive(true);
                    Debug.Log("End");
                    break;

                case State.TIntersectionDialogueRightLeft:
                    hideTIntersectionLeftDialogue.gameObject.SetActive(false);
                    hideTIntersectionRightDialogue.gameObject.SetActive(false);
                    hideIntersectionDialogue.gameObject.SetActive(false);
                    hideTutorial.gameObject.SetActive(false);
                    hideEnd.gameObject.SetActive(false);
                    hideTIntersectionDialogueRightLeft.gameObject.SetActive(true);
                    TriggerChoiceMaking();
                    Debug.Log("T intersection right left");
                    break;
            }
        }

    }
    public void TriggerTutorial ()
    {     
        FindFirstObjectByType<DialogueManager>().StartDialogue(tutorial);
        
    }
    public void TriggerChoiceMaking()
    {
        FindFirstObjectByType<DialogueManager>().StartDialogue(choiceMaking);
    }

    public void DialogEnded()
    {
        dialogueStarted = false;
        player.GoToState(MotorcycleMovement.State.UserControled);                              
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Intersection"))
        {
            currentDialogueState = State.IntersectionDialogue;
            Debug.Log("Intersection");
        }
        else if (other.CompareTag("TIntersection"))
        {

            if (other.transform.rotation.eulerAngles.y == 90)
            {
                currentDialogueState = State.TIntersectionLeft;
                Debug.Log("TIntersectionToLeft");
            }
            else if (other.transform.rotation.eulerAngles.y == 270)
            {
                currentDialogueState = State.TIntersectionRight;
                Debug.Log("TIntersectionToRight");
            }
            else if (other.transform.rotation.eulerAngles.y == 0)
            {
                currentDialogueState = State.TIntersectionDialogueRightLeft;
                Debug.Log("TIntersectionToLeftRight");
            }
        }
        else if (other.CompareTag("End"))
        {
            Debug.Log("End");
            currentDialogueState = State.EndDialogue;
        }
        else if (other.CompareTag("Corner"))
        {
            Debug.Log("Corner");
            currentDialogueState = State.CornerDialogue;
        }
    }
    public State getCurrentDialogueState()
    {
        return currentDialogueState;
    }
}
