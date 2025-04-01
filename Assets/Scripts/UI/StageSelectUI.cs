using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : BaseUI
{
    public Button seoulButton;
    public Button deajonButton;
    public Button deaguButton;
    public Button busanButton;
    public Button exitButton;

    public BaseStageSelectUI seoulStage;
    public BaseStageSelectUI deajonStage;
    public BaseStageSelectUI deaguStage;
    public BaseStageSelectUI busanStage;


    private void Awake()
    {
        seoulButton.interactable = false;
        deajonButton.interactable = false;
        deaguButton.interactable = false;
        
    }

    public void Update()
    {
        //if(busanStage.clearedStages.Contains(7))
        //{
        //    deaguButton.interactable = true;
        //}
        
        //if(deaguStage.clearedStages.Contains(7))
        //{
        //    deajonButton.interactable = true;
        //}

        //if(deajonStage.clearedStages.Contains(7))
        //{
        //    seoulButton.interactable = true;
        //}
        
    }
    protected override UIState GetUIState()
    {
        return UIState.Stage;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        exitButton.onClick.AddListener(OnClickExit);
    }


    public void OnClickExit()
    {
        uiManager.OnClickLobby();
        seoulStage.gameObject.SetActive(false);
        deajonStage.gameObject.SetActive(false);
        deaguStage.gameObject.SetActive(false);
        busanStage.gameObject.SetActive(false);
    }
}
