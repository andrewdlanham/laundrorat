using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    private int currentScore;

    void Awake() {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        currentScore = PlayerPrefs.GetInt("Score");
        UpdateScoreText();
        UpdateHighScoreText();
    }

    
    public void AddPointsToScore(int numPoints) {
        currentScore += numPoints;
        PlayerPrefs.SetInt("Score", currentScore);
        UpdateScoreText();
        UpdateHighScore();
        UpdateHighScoreText();
    }

    private void UpdateScoreText() {
        scoreText.text = currentScore.ToString();
    }

    private void UpdateHighScoreText() {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void UpdateHighScore() {
        int oldHighScore = PlayerPrefs.GetInt("HighScore");
        if (currentScore > oldHighScore) {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }
}
