using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class EndSceneManager : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    
    void Start()
    {
        StartCoroutine(EndSceneCutscene());
    }

    private void SetScoreText() {
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    private void ShowHighScoreText() {
        highScoreText.gameObject.SetActive(true);
    }

    private IEnumerator EndSceneCutscene() {
        SetScoreText();
        Debug.Log("Player HighScore: " + PlayerPrefs.GetInt("HighScore"));
        if (PlayerPrefs.GetInt("Score") == PlayerPrefs.GetInt("HighScore")) {
            ShowHighScoreText();
        }
        yield return new WaitForSeconds(5f);
        Application.Quit();
        //SceneManager.LoadScene("MainMenu");
    }
    
}
