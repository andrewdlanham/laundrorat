using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesManager : MonoBehaviour
{
    
    private TextMeshProUGUI livesText;
    public int numLives;

    void Awake() {
        numLives = PlayerPrefs.GetInt("Lives");
        livesText = GameObject.Find("LivesText").GetComponent<TextMeshProUGUI>();
        UpdateLivesText();
    }

    public void LoseALife() {
        numLives--;
        PlayerPrefs.SetInt("Lives", numLives);
        UpdateLivesText();
    }

    public void UpdateLivesText() {
        livesText.text = "L: " + numLives.ToString();
    }
}
