using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    internal float time=5f;
    public TextMeshProUGUI CountDown;
    DialogueManager manager;
    MotorcycleMovement player;
    internal bool isRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       manager = FindFirstObjectByType<DialogueManager>();
        player = FindFirstObjectByType<MotorcycleMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.getCurrentState() == MotorcycleMovement.State.Dialogue)
        {
            time -= Time.deltaTime;
            time = Mathf.Max(time, 0f);
            CountDown.text = "00:0" + Mathf.CeilToInt(time);
            if (time <= 0)
            {

                CountDown.text = "00:00";

                if (manager.currentState == DialogueTrigger.State.TIntersectionDialogueRightLeft)
                {
                    Debug.Log("Time up. Turning right");
                    manager.turnRight();
                }

                manager.EndDialogue();
                Reset();


            }
            
        }
        
    }
    public void StartTimer()
    {
        time = 5f;
        isRunning = true;
        gameObject.SetActive(true);
    }
    internal void Reset()
    {
        isRunning = false;
        time = 5f;
       CountDown.text = "00:0" + Mathf.CeilToInt(time);
        gameObject.SetActive(false);
    }

}
