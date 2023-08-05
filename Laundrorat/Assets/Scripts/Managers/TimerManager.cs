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
        DontDestroyOnLoad(gameObject);
        //InitializeTimer(99f);
    }

    void Update()
    {
        if (!timeIsUp) {
            currentTime -= 1 * Time.deltaTime;
            int roundedTime = Mathf.CeilToInt(currentTime);
            if (roundedTime == 0) {
                timeIsUp = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().HurryUp();
            }
            timerText.text = roundedTime.ToString();
        }
    }

    public void InitializeTimer(float numSeconds) {
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        currentTime = numSeconds;
        timeIsUp = false;
    }

    
}
