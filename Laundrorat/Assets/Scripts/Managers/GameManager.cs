using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    private UIManager uiManager;

    public int scoreMultiplier;

    void Awake() {

        SetPlayerReference();
        SetManagerReferences();
        scoreMultiplier = 2;

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        StartCoroutine(StageNumCutscene(1));
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
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void FinishLevel() {
        playerController.enabled = false;
        collectableManager.enabled = false;
    }

    public void TriggerDeathSequence() {
        TestLoadNextLevel();
        return;
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

    private void UnfreezeLevel() {
        Debug.Log("UnfreezeLevel()");
        playerController.enabled = true;
        timerManager.enabled = true;
        enemyManager.EnableAllEnemies();
    }

    public void TestLoadNextLevel() {
        SceneManager.LoadScene("Level2");
    }

    public void HurryUp() {
        Debug.Log("HurryUp()");
        StartCoroutine(HurryUpCutscene());
    }

    IEnumerator HurryUpCutscene() {
        Debug.Log("HurryUpCutscene()");
        FreezeLevel();
        uiManager.ShowHurryUpText();
        audioManager.PlaySound("HurryUp");
        yield return new WaitForSeconds(2f);
        enemyManager.SpeedUpAllEnemies();
        uiManager.HideHurryUpText();
        UnfreezeLevel();
    }

    IEnumerator StageNumCutscene(int stageNum) {
        Debug.Log("StageNumCutscene()");
        FreezeLevel();
        uiManager.ShowStageNum(stageNum);
        yield return new WaitForSeconds(2f);
        uiManager.HideStageNum();
        yield return new WaitForSeconds(1f);
        UnfreezeLevel();
    }
}
