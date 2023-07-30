using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;
    private TextMeshProUGUI timerText;


    private float currentTime;

    private bool timeIsUp;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        InitializeTimer(60);
        DontDestroyOnLoad(gameObject);
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

    public void InitializeTimer(int numSeconds) {
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        currentTime = numSeconds;
        timeIsUp = false;
    }
}
