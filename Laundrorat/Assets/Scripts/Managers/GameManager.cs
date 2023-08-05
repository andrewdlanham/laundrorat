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
    private StageManager stageManager;

    private ScoreManager scoreManager;

    private CameraFollow cameraFollow;

    public int scoreMultiplier;

    void Awake() {

        SetScriptReferences();
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
        //stageManager.SetUpStage();
        //StartCoroutine(StageNumCutscene(stageManager.stageNum));
        cameraFollow.enabled = true;
        cameraFollow.SetPlayerReference();
    }

    private void SetScriptReferences() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        collectableManager = GameObject.Find("CollectableManager").GetComponent<CollectableManager>();
        livesManager = GameObject.Find("LivesManager").GetComponent<LivesManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        timerManager = GameObject.Find("TimerManager").GetComponent<TimerManager>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    public void StageClear() {
        StartCoroutine(StageClearCutscene());
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
        StartCoroutine(ReloadCurrentSceneWithDelay());
    }

    IEnumerator ReloadCurrentSceneWithDelay() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        FreezeLevel();
        yield return new WaitForSeconds(3f);
        livesManager.InitializeLivesText();
        stageManager.SetUpStage();
        UnfreezeLevel();

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

    public void HurryUp() {
        Debug.Log("HurryUp()");
        StartCoroutine(HurryUpCutscene());
    }

    private void ProceedToNextStage() {
        Debug.Log("ProceedToNextStage()");
        playerController.ResetPlayerPosition();
        stageManager.LoadNextStage();
        //StartCoroutine(StageNumCutscene(stageManager.stageNum));   
    }

    #region Cutscenes
    

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

    IEnumerator GameOverCutscene() {
        Debug.Log("GameOverCutscene()");
        FreezeLevel();
        uiManager.ShowGameOverText();
        audioManager.PlaySound("PlayerDeath");
        yield return new WaitForSeconds(3f);
        uiManager.HideGameOverText();
        cameraFollow.enabled = false;
        SceneManager.LoadScene("EndScene");
    }

    IEnumerator DeathCutscene() {
        Debug.Log("DeathCutscene()");
        FreezeLevel();
        audioManager.PlaySound("PlayerDeath");
        yield return new WaitForSeconds(3f);
        playerController.ResetPlayerPosition();
        ReloadCurrentScene();
    }

    IEnumerator StageClearCutscene() {
        Debug.Log("StageClearCutscene()");
        FreezeLevel();
        yield return new WaitForSeconds(0.5f);
        audioManager.PlaySound("StageClear");
        uiManager.ShowStageClearedGO();
        yield return new WaitForSeconds(2f);
        uiManager.HideStageClearedGO();
        ProceedToNextStage();
    }

    #endregion

    public void ResetScoreMultiplier() {
        scoreMultiplier = 2;
    }
}
