using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI HurryUpText;

    [SerializeField] private GameObject StageNumPanel;
    [SerializeField] private GameObject StageNumTextGO;

    [SerializeField] private GameObject StageClearedGO;
    private TextMeshProUGUI StageNumText;
    
    void Awake() {
        StageNumText = StageNumTextGO.GetComponent<TextMeshProUGUI>();
        HideHurryUpText();
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

}
