using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI HurryUpText;

    [SerializeField] private GameObject StageNumPanel;
    [SerializeField] private GameObject StageNumTextGO;

    [SerializeField] private GameObject StageClearedGO;

    [SerializeField] private GameObject GameOverTextGO;

    private TextMeshProUGUI StageNumText;

    private StageManager stageManager;
    
    void Awake() {
        Debug.Log("UIManager Awake()");
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
    }

    void Start() {
        Debug.Log("UIManager Start()");
    }

    public void InitializeUI() {
        Debug.Log("UIManager.InitializeUI()");
        HurryUpText = GameObject.Find("HurryUpText").GetComponent<TextMeshProUGUI>();
        StageNumPanel = GameObject.Find("StageNumPanel");
        StageClearedGO = GameObject.Find("StageClearedGO");
        GameOverTextGO = GameObject.Find("GameOverGO");
        StageNumTextGO = GameObject.Find("StageNumText");

        StageNumText = StageNumTextGO.GetComponent<TextMeshProUGUI>();

        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        HideHurryUpText();
        HideGameOverText();
        HideStageClearedGO();
        HideStageNum();
    }

    public void ShowHurryUpText() {
        HurryUpText.gameObject.SetActive(true);
    }

    public void HideHurryUpText() {
        HurryUpText.gameObject.SetActive(false);
    }

    public void ShowStageNum(int stageNum) {
        StageNumText.text = "STAGE " + stageNum.ToString();
        StageNumPanel.SetActive(true);
        StageNumTextGO.SetActive(true);
    }

    public void HideStageNum() {
        StageNumPanel.SetActive(false);
        StageNumTextGO.SetActive(false);
    }

    public void ShowStageClearedGO() {
        StageClearedGO.SetActive(true);
    }

    public void HideStageClearedGO() {
        StageClearedGO.SetActive(false);
    }

    public void ShowGameOverText() {
        GameOverTextGO.SetActive(true);
    }

    public void HideGameOverText() {
        GameOverTextGO.SetActive(false);
    }

    public void StartStageNumCutscene() {
        StartCoroutine(StageNumCutscene());
    }

    IEnumerator StageNumCutscene() {
        Debug.Log("StageNumCutscene()");
        //FreezeLevel();
        ShowStageNum(stageManager.stageNum);
        yield return new WaitForSeconds(2f);
        HideStageNum();
        yield return new WaitForSeconds(1f);
        //UnfreezeLevel();
    }

}
