using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    public Button jumagButton;
    public Button healPopupButton;
    public Button trainingButton;
    public Button smithyButton;
    public Button stageSelectButton;
    public Button settingButton;
    public Button infoButton;

    public TextMeshProUGUI goldText;

    public GameObject healPopup;

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        jumagButton.onClick.AddListener(OnClickJumag);
        healPopupButton.onClick.AddListener(OnClickHealPopupWindow);
        trainingButton.onClick.AddListener(OnClickTraining);
        smithyButton.onClick.AddListener(OnClickSmith);
        stageSelectButton.onClick.AddListener(OnClickStage);

        settingButton.onClick.AddListener(OnClickSetting);
        infoButton.onClick.AddListener(OnClickInfo);
    }

    private void OnEnable()
    {
        if (uiManager != null)
        {
            uiManager.OnGoldChanged += UpdateGoldUI;
            UpdateGoldUI(uiManager.GetGold());
        }  
    }

    private void OnDisable()
    {
        if(uiManager != null)
        {
            uiManager.OnGoldChanged -= UpdateGoldUI;
        }

    }

    private void UpdateGoldUI(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void OnClickJumag()
    {
        uiManager.OnClickJumag();
        uiManager.GeneratePartner();
    }

    public void OnClickHealPopupWindow()
    {
        healPopup.SetActive(true);
    }

    public void OnClickTraining()
    {
        uiManager.OnClickTraining();
        uiManager.SetFriendlyPartyListView();
        uiManager.UpgreadTrainingUI();
    }

    public void OnClickSmith()
    {
        uiManager.OnClickSmith();
    }

    public void OnClickStage()
    {
        uiManager.OnClickStartStage();
    }

    public void OnClickSetting()
    {
        uiManager.OnClickSettingButton();
    }

    public void OnClickInfo()
    {

    }


}
