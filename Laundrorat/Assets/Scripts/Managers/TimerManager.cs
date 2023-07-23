using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    
    private TextMeshProUGUI timerText;


    private float currentTime = 0f;
    private float startingTime = 60f;

    void Awake() {
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        int roundedTime = Mathf.CeilToInt(currentTime);
        timerText.text = roundedTime.ToString();
    }
}
