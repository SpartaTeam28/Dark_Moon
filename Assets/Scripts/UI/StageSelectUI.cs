using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectUI : BaseUI
{
    public Button seoulButton;
    public Button deajonButton;
    public Button deaguButton;
    public Button busanButton;
    public Button exitButton;

    //public BaseStageSelectUI seoulStage;
    //public BaseStageSelectUI deajonStage;
    //public BaseStageSelectUI deaguStage;
    //public BaseStageSelectUI busanStage;


    private void Awake()
    {
        seoulButton.interactable = false;
        deajonButton.interactable = false;
        deaguButton.interactable = false;
        
    }

    protected override UIState GetUIState()
    {
        return UIState.Stage;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        seoulButton.onClick.AddListener(() => OnClickStageButton(SelectStageName.Seoul));
        deajonButton.onClick.AddListener(() => OnClickStageButton(SelectStageName.Deajon));
        deaguButton.onClick.AddListener(() => OnClickStageButton(SelectStageName.Deagu));
        busanButton.onClick.AddListener(() => OnClickStageButton(SelectStageName.Busan));
        exitButton.onClick.AddListener(OnClickExit);
    }


    public void OnClickStageButton(SelectStageName stageName)
    {
        UIManager.instance.SetCurrentStageName(stageName, 0);

        switch(stageName)
        {
            case SelectStageName.Busan:
                SceneManager.LoadScene("Battle_Scene_Busan");
                break;
            case SelectStageName.Deagu:
                SceneManager.LoadScene("Battle_Scene_Dague");
                break;
            case SelectStageName.Deajon:
                SceneManager.LoadScene("Battle_Scene_Hanbat");
                break;
            case SelectStageName.Seoul:
                SceneManager.LoadScene("Battle_Scene_Seoul");
                break;
        }

        UIManager.instance.OnClickCommonBattle();
    }


    public void OnClickExit()
    {
        uiManager.OnClickLobby();
        //seoulStage.gameObject.SetActive(false);
        //deajonStage.gameObject.SetActive(false);
        //deaguStage.gameObject.SetActive(false);
        //busanStage.gameObject.SetActive(false);
    }

    public void EnableDaeguButton()
    {
        deaguButton.interactable = true;

    }
    public void EnableUlsanButton()
    {
        //deaguButton.interactable = true;
    }
}
