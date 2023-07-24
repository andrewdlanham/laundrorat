using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    
    private TextMeshProUGUI timerText;


    private float currentTime = 0f;
    private float startingTime = 10f;

    private bool timeIsUp;

    void Awake() {
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        timeIsUp = false;
    }

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if (!timeIsUp) {
            currentTime -= 1 * Time.deltaTime;
            int roundedTime = Mathf.CeilToInt(currentTime);
            if (roundedTime == 0) timeIsUp = true;
            timerText.text = roundedTime.ToString();
        }
    }
}
