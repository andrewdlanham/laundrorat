using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private PlayerController playerController;
    private CollectableManager collectableManager;
    private LivesManager livesManager;
    private AudioManager audioManager;
    private TimerManager timerManager;

    void Awake() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        collectableManager = GameObject.Find("CollectableManager").GetComponent<CollectableManager>();
        livesManager = GameObject.Find("LivesManager").GetComponent<LivesManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        timerManager = GameObject.Find("TimerManager").GetComponent<TimerManager>();
    }

    public void FinishLevel() {
        DisableCollectableManager();
        DisablePlayerController();
    }

    private void DisablePlayerController() {
        playerController.enabled = false;
    }

    private void DisableCollectableManager() {
        collectableManager.enabled = false;
    }

    public void TriggerDeathSequence() {
        Debug.Log("DEATH");
        livesManager.LoseALife();
        if (livesManager.numLives < 1) {
            GameOver();
        } else {
            FreezeLevel();
            StartCoroutine(DeathCutscene());
            
        }
    }

    private void ReloadCurrentScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void GameOver() {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator DeathCutscene() {
        Debug.Log("DeathCutscene");
        audioManager.PlaySound("PlayerDeath");
        yield return new WaitForSeconds(3f);
        ReloadCurrentScene();
    }

    private void FreezeLevel() {
        playerController.enabled = false;
        timerManager.enabled = false;
    }
}
