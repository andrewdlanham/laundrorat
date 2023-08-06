using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{

    public static StageManager instance;

    public int stageNum;
    [SerializeField] private int stageNamesIndex;

    private List<float[]> stageInfo;
    private List<string> stageNames;

    // Managers
    private TimerManager timerManager;
    private EnemyManager enemyManager;
    private CollectableManager collectableManager;

    private UIManager uiManager;
    private ScoreManager scoreManager;

    private GameManager gameManager;
    private LivesManager livesManager;

    void Awake()
    {
        Debug.Log("StageManager Awake()");
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SetManagerReferences();
        PopulateStageInfo();
        PopulateStageNames();

        stageNum = 1;
        stageNamesIndex = 1;

        SetUpStage();

    }

    #region Initialization
    private void SetManagerReferences()
    {
        collectableManager = GameObject.Find("CollectableManager").GetComponent<CollectableManager>();
        timerManager = GameObject.Find("TimerManager").GetComponent<TimerManager>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        livesManager = GameObject.Find("LivesManager").GetComponent<LivesManager>();

    }

    private void PopulateStageInfo()
    {
        Debug.Log("PopulateStageInfo()");
        stageInfo = new List<float[]> { };
        stageInfo.Add(new float[3] { 0f, 0f, 0f });    // This entry should never be accessed since there is no Stage 0
        stageInfo.Add(new float[3] { 1.4f, 2.5f, 60f }); // Stage 1.1
        stageInfo.Add(new float[3] { 1.4f, 2.5f, 60f }); // Stage 1.2
        stageInfo.Add(new float[3] { 1.6f, 2.7f, 50f }); // Stage 2.1
        stageInfo.Add(new float[3] { 1.6f, 2.7f, 50f }); // Stage 2.2
        stageInfo.Add(new float[3] { 1.8f, 2.9f, 40f }); // Stage 3.1
        stageInfo.Add(new float[3] { 1.8f, 2.9f, 40f }); // Stage 3.2
        stageInfo.Add(new float[3] { 2f, 3f, 35f }); // Stage 1.3
        stageInfo.Add(new float[3] { 2f, 3f, 35f }); // Stage 1.4
        stageInfo.Add(new float[3] { 2.25f, 3.25f, 30f }); // Stage 2.3
        stageInfo.Add(new float[3] { 2.25f, 3.25f, 30f }); // Stage 2.4

        stageInfo.Add(new float[3] { 2.5f, 3.5f, 30f }); // All stages afterwards
    }

    private void PopulateStageNames()
    {
        stageNames = new List<string> { };
        stageNames.Add("Stage1.1");
        stageNames.Add("Stage1.2");
        stageNames.Add("Stage2.1");
        stageNames.Add("Stage2.2");

        stageNames.Add("Stage3.1");
        stageNames.Add("Stage3.2");
        stageNames.Add("Stage1.3");
        stageNames.Add("Stage1.4");
        stageNames.Add("Stage2.3");
        stageNames.Add("Stage2.4");

    }
    #endregion

    public void SetUpStage()
    {
        Debug.Log("SetUpStage()");
        Debug.Log("stageNum: " + stageNum);

        livesManager.InitializeLivesText();
        timerManager.InitializeTimer(stageInfo[stageNum][2]);
        collectableManager.InitializeCollectableObjectsList();
        collectableManager.triggerStageClear = false;
        scoreManager.Initialize();
        
        uiManager.Initialize();
        uiManager.HideConditionalUI();

        enemyManager.InitializeEnemyObjectsList();
        enemyManager.SetSpeedAllEnemies(stageInfo[stageNum][0], stageInfo[stageNum][1]);

        gameManager.Initialize();
        gameManager.FreezeLevel();
        gameManager.ResetScoreMultiplier();
        
        uiManager.StartStageNumCutscene();
    }

    public void LoadNextStage()
    {
        StartCoroutine(LoadNextStageWithDelay());
    }

    private IEnumerator LoadNextStageWithDelay()
    {
        if (stageNamesIndex == 10) stageNamesIndex = 4;
        if (stageNum != 11) stageNum++;
        Debug.Log("Loading " + stageNames[stageNamesIndex]);
        SceneManager.LoadScene(stageNames[stageNamesIndex]);
        stageNamesIndex++;
        yield return new WaitForSeconds(0.1f);
        SetUpStage();
    }

    public void ReloadCurrentStage() {
        StartCoroutine(ReloadCurrentStageWithDelay());
    }

    private IEnumerator ReloadCurrentStageWithDelay() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        yield return new WaitForSeconds(0.1f);
        SetUpStage();
    }

    



}
