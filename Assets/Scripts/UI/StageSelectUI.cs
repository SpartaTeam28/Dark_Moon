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

    public GameObject seoulStage;
    public GameObject deajonStage;
    public GameObject deaguStage;
    public GameObject busanStage;

    protected override UIState GetUIState()
    {
        return UIState.Stage;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        seoulButton.onClick.AddListener(OnClickSeoilStage);
        deajonButton.onClick.AddListener(OnClickDeajonStage);
        deaguButton.onClick.AddListener(OnClickDeajonStage);
        busanButton.onClick.AddListener(OnClickBusanStage);
        exitButton.onClick.AddListener(OnClickExit);
    }


    public void OnClickSeoilStage()
    {
        seoulStage.SetActive(true);
    }
    public void OnClickDeajonStage()
    {
        deaguStage.SetActive(true);
    }

    public void OnClickDeaguStage()
    {
        deaguStage.SetActive(true);
    }

    public void OnClickBusanStage()
    {
        busanStage.SetActive(true);
    }



    public void OnClickExit()
    {
        uiManager.OnClickLobby();
    }
}
