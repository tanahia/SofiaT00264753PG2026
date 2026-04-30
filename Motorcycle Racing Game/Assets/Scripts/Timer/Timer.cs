using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    internal float time=5f;
    public TextMeshProUGUI CountDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        }
    }
}
