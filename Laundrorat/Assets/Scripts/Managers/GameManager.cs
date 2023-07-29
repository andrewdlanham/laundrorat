using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Collectable flashingCollectable;


    private PlayerController playerController;

    // Manager References
    private CollectableManager collectableManager;
    private LivesManager livesManager;
    private AudioManager audioManager;
    private TimerManager timerManager;
    private EnemyManager enemyManager;

    void Awake() {

        SetPlayerReference();
        SetManagerReferences();

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        
    }

    private void SetPlayerReference() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    

    private void SetManagerReferences() {
        collectableManager = GameObject.Find("CollectableManager").GetComponent<CollectableManager>();
        livesManager = GameObject.Find("LivesManager").GetComponent<LivesManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        timerManager = GameObject.Find("TimerManager").GetComponent<TimerManager>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    }

    public void FinishLevel() {
        playerController.enabled = false;
        collectableManager.enabled = false;
    }

    public void TriggerDeathSequence() {
        
        Debug.Log("TriggerDeathSequence()");
        livesManager.LoseALife();
        if (livesManager.numLives < 1) {
            StartCoroutine(GameOverCutscene());
        } else {
            StartCoroutine(DeathCutscene());
        }
    }

    private void ReloadCurrentScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator GameOverCutscene() {
        Debug.Log("GameOverCutscene()");
        FreezeLevel();
        audioManager.PlaySound("PlayerDeath");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator DeathCutscene() {
        Debug.Log("DeathCutscene()");
        FreezeLevel();
        audioManager.PlaySound("PlayerDeath");
        yield return new WaitForSeconds(3f);
        ReloadCurrentScene();
    }

    private void FreezeLevel() {
        Debug.Log("FreezeLevel()");
        playerController.enabled = false;
        timerManager.enabled = false;
        enemyManager.DisableAllEnemies();
    }

    public void TestLoadNextLevel() {
        SceneManager.LoadScene("Level2");
    }
}
