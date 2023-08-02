using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadLevel1() {
        ResetLives();
        ResetScore();
        SceneManager.LoadScene("Stage1.1");
    }

    private void ResetLives() {
        PlayerPrefs.SetInt("Lives", 3);
    }

    private void ResetScore() {
        PlayerPrefs.SetInt("Score", 0);
    }
}
