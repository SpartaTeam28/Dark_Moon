using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommonBattleUI : BaseUI
{
    public static CommonBattleUI instance;


    public TextMeshProUGUI nameText;
    public TextMeshProUGUI jobText;
    public TextMeshProUGUI lvText;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenceText;
    public TextMeshProUGUI criticalText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI evasionText;
    public TextMeshProUGUI speedText;



    public GameObject statPanel;
    public GameObject clearPanel;
    public GameObject defeatPanel;

    public List<Character> battleCharacters;
    //public List<Character> enemyCharacters;

    public SelectStageName currentStageName;
    public Button nextButton;
    public int currentStagenumber = 0;

    protected override UIState GetUIState()
    {
        return UIState.CommonBattle;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //battleCharacters = GameManager.instance.friendlyCharacterList;
        //enemyCharacters = GameManager.instance.EnemyCharacterList;
        ShowUI(false); // 게임 시작 시 UI 숨기기
    }


    public void SetCharacterData(Character anonymouscharacter)
    {
        nameText.text = anonymouscharacter.info.characterName;
        jobText.text = anonymouscharacter.info.job.jobName;
        healthText.text = anonymouscharacter.stat.health.GetValueToString();
        manaText.text = anonymouscharacter.stat.mana.GetValueToString();
        attackText.text = anonymouscharacter.stat.attack.GetValueToString();
        defenceText.text = anonymouscharacter.stat.defence.GetValueToString();
        criticalText.text = anonymouscharacter.stat.critical.GetValueToString();
        accuracyText.text = anonymouscharacter.stat.accuracy.GetValueToString();
        evasionText.text = anonymouscharacter.stat.evasion.GetValueToString();
        speedText.text = anonymouscharacter.stat.speed.GetValueToString();

    }

    // UI 표시/숨김 기능 추가
    public void ShowUI(bool isVisible)
    {
        if (statPanel != null)
        {
            statPanel.SetActive(isVisible);
        }
    }

    public void OnClickWinButton()
    {
        uiManager.OnEnableStageButton(currentStageName, currentStagenumber);
        SceneManager.LoadScene("YGM_Scene");
        uiManager.OnClickLobby();
      
    }

    public void OnClickDefeatButton()
    {
        SceneManager.LoadScene("YGM_Scene");
        uiManager.OnClickLobby();
    }


}
