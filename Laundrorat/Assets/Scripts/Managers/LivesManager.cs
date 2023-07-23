using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesManager : MonoBehaviour
{
    
    private TextMeshProUGUI livesText;
    private int numLives;

    void Awake() {
        numLives = 3;
        livesText = GameObject.Find("LivesText").GetComponent<TextMeshProUGUI>();
        UpdateLivesText();
    }

    public void LoseALife() {
        numLives--;
        UpdateLivesText();
    }

    public void UpdateLivesText() {
        livesText.text = "L: " + numLives.ToString();;
    }
}
