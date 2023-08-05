using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{

    public static StageManager instance;

    public int stageNum;
    [SerializeField] private int stageNamesIndex;
    private int stageInfoIndex;

    private bool loopingStages;

    private List<float[]> stageInfo;
    private List<string> stageNames;

    // Managers
    private TimerManager timerManager;
    private EnemyManager enemyManager;
    private CollectableManager collectableManager;

    private PlayerController playerController;
    private UIManager uiManager;
    private ScoreManager scoreManager;

    private GameManager gameManager;

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

        SetPlayerReference();
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
        //livesManager = GameObject.Find("LivesManager").GetComponent<LivesManager>();
        //audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        timerManager = GameObject.Find("TimerManager").GetComponent<TimerManager>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void SetPlayerReference()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void PopulateStageInfo()
    {
        Debug.Log("PopulateStageInfo()");
        stageInfo = new List<float[]> { };
        stageInfo.Add(new float[3] { 0f, 0f, 0f });    // This entry should never be accessed since there is no Stage 0
        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // Stage 1.1
        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // Stage 1.2
        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // Stage 2.1
        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // Stage 2.2
        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // Stage 3.1
        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // Stage 3.2
        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // Stage 1.3
        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // Stage 1.4
        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // Stage 2.3
        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // Stage 2.4

        stageInfo.Add(new float[3] { 1.5f, 3f, 35f }); // All stages afterwards
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

        uiManager.InitializeUI();
        scoreManager.InitializeUI();

        collectableManager.InitializeCollectableObjectsList();
        collectableManager.triggerStageClear = false;

        enemyManager.InitializeEnemyObjectsList();
        timerManager.InitializeTimer(stageInfo[stageNum][2]);

        enemyManager.SetSpeedAllEnemies(stageInfo[stageNum][0], stageInfo[stageNum][1]);

        gameManager.ResetScoreMultiplier();

        // TODO: Start stage num cutscene
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

        yield return new WaitForSeconds(2f);
        SetUpStage();
    }



}
