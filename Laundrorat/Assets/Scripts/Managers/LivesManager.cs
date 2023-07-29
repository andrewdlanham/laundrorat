using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;
    private TextMeshProUGUI livesText;
    public int numLives;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        numLives = PlayerPrefs.GetInt("Lives");
        livesText = GameObject.Find("LivesText").GetComponent<TextMeshProUGUI>();
        UpdateLivesText();
        DontDestroyOnLoad(gameObject);
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
