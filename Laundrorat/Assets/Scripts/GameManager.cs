using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerController playerController;
    private CollectableManager collectableManager;

    void Awake() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        collectableManager = GameObject.Find("CollectableManager").GetComponent<CollectableManager>();
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
}
