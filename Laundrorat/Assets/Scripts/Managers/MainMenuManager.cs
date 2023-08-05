using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    void Awake() {
        if(GameObject.Find("UI Canvas")) {
            GameObject.Find("UI Canvas").SetActive(false);
        }
    }
    
    public void LoadLevel1() {
        ResetLives();
        ResetScore();
        SceneManager.LoadScene("Stage1.1");
    }

    public void LoadLevel1WithCheats() {
        PlayerPrefs.SetInt("Lives", 99);
        SceneManager.LoadScene("Stage1.1");
    }

    private void ResetLives() {
        PlayerPrefs.SetInt("Lives", 3);
    }

    private void ResetScore() {
        PlayerPrefs.SetInt("Score", 0);
    }
}
