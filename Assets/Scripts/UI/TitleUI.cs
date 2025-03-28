using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : BaseUI
{

    public Button startButton;
    public Button settingButton;
    public Button exitButton;
    protected override UIState GetUIState()
    {
        return UIState.Title;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        startButton.onClick.AddListener(OnClickStartButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickSettingButton()
    {

    }

    public void OnClickStartButton()
    {
        uiManager.OnClickStart();
        uiManager.prevState = UIState.Title;
    }

    public void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }
}
