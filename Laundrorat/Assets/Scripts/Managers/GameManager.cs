using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private PlayerController playerController;
    private CollectableManager collectableManager;
    private LivesManager livesManager;

    void Awake() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        collectableManager = GameObject.Find("CollectableManager").GetComponent<CollectableManager>();
        livesManager = GameObject.Find("LivesManager").GetComponent<LivesManager>();
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
        ReloadCurrentScene();
    }

    private void ReloadCurrentScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
