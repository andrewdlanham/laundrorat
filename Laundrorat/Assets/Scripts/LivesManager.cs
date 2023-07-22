using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesManager : MonoBehaviour
{
    
    private TextMeshProUGUI livesText;
    private int numLives = 3;

    void Awake() {
        livesText = GameObject.Find("LivesText").GetComponent<TextMeshProUGUI>();
        livesText.text = "L: " + numLives.ToString();;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
