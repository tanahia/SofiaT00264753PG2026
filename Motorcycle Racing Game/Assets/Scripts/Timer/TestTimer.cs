using TMPro;
using UnityEngine;

public class TestTimer : MonoBehaviour
{
    internal float time = 5f;
    public TextMeshProUGUI CountDown;
    internal bool isRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
     
            time -= Time.deltaTime;
            time = Mathf.Max(time, 0f);
            CountDown.text = "00:0" + Mathf.CeilToInt(time);
            if (time <= 0)
            {

                CountDown.text = "00:00";
                Reset();
        }

    }
    public void StartTimer()
    {
       
        isRunning = true;
        gameObject.SetActive(true);
    }
    internal void Reset()
    {
        isRunning = false;
        float timeAfter = time;
        CountDown.text = "00:0" + Mathf.CeilToInt(time);
        gameObject.SetActive(false);
    }

}
