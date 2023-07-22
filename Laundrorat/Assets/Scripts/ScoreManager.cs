using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    private TextMeshProUGUI scoreText;
    private int currentScore;

    void Awake() {
        currentScore = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        AddPointsToScore(0);
    }

    
    void AddPointsToScore(int numPoints) {
        currentScore += numPoints;
        scoreText.text = currentScore.ToString();
    }
}
