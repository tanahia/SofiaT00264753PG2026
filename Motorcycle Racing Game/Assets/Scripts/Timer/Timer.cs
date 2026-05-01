using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    internal float time=5f;
    public TextMeshProUGUI CountDown;
    DialogueManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       manager = FindFirstObjectByType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        CountDown.text = "00:"+(int)time;
        if (time <= 0)
            {
                time = 0;
                CountDown.text = "00:00";
            if (manager.currentState == DialogueTrigger.State.IntersectionDialogue)
            {

                manager.turnRight();


            } else
                manager.EndDialogue();
           
            time = 5f;
            CountDown.text = "00:" + (int)time;
            gameObject.SetActive(false);
        }
    }
}
