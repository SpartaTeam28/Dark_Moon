using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public enum UIState
{
    Title, //0
    CharacterSelect, // 1
    Lobby, // 2
    Jumag, // 3
    HealStation, //4
    Training, //5
    Smith, //6
    Stage, //7
    CommonBattle //8
}



public class UIManager : MonoBehaviour
{
    public UIState currentState = UIState.Title;
    public UIState prevState = UIState.Title;

    TitleUI titleUI = null;
    CharacterSelectUI characterSelectUI = null;
    LobbyUI lobbyUI = null;
    JumagUI jumagUI = null;
    //HealStationUI healStationUI = null;
    TrainingUI trainingUI = null;
    SmithUI smithUI = null;
    StageSelectUI stageSelectUI = null;
    CommonBattleUI commonBattleUI = null;

    public SettingPopupUI settingPopupUI = null;

    public List<Character> partnerCharacters;// 구매한 캐릭터 리스트

    public event Action<int> OnGoldChanged;
    private int gold = 10000;


    private static UIManager _instance;

    public bool isNotStart;

    public static UIManager instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (_instance != this)
                Destroy(this.gameObject);
        }

        titleUI = GetComponentInChildren<TitleUI>(true);
        titleUI?.Init(this);
        characterSelectUI = GetComponentInChildren<CharacterSelectUI>(true);
        characterSelectUI.Init(this);
        lobbyUI = GetComponentInChildren<LobbyUI>(true);
        lobbyUI.Init(this);
        jumagUI = GetComponentInChildren<JumagUI>(true);
        jumagUI?.Init(this);
        //healStationUI = GetComponentInChildren<HealStationUI>(true);
        //healStationUI?.Init(this);
        trainingUI = GetComponentInChildren<TrainingUI>(true);
        trainingUI?.Init(this);
        smithUI = GetComponentInChildren<SmithUI>(true);
        smithUI?.Init(this);
        stageSelectUI = GetComponentInChildren<StageSelectUI>(true);
        stageSelectUI?.Init(this);
        commonBattleUI = GetComponentInChildren<CommonBattleUI>(true);
        commonBattleUI?.Init(this);

        settingPopupUI = GetComponentInChildren<SettingPopupUI>(true);
    }

    private void Start()
    {
        if (!isNotStart) 
            ChangeState(currentState);
    }

    public void ChangeState(UIState state) //UI오브젝트를 on off 해주는 기능
    {
        currentState = state; //아래에서 해당하는 UI오브젝트를 찾아 on off 해줌

        titleUI?.SetActive(currentState);
        characterSelectUI?.SetActive(currentState);
        lobbyUI?.SetActive(currentState);
        jumagUI?.SetActive(currentState);
        //healStationUI?.SetActive(currentState);
        trainingUI?.SetActive(currentState);
        smithUI?.SetActive(currentState);
        stageSelectUI?.SetActive(currentState);
        commonBattleUI?.SetActive(currentState);
    }

    public void OnClickStart() //시작하기를 누른경우
    {
        ChangeState(UIState.CharacterSelect);
    }

    public void OnClickExit() //게임 종료를 누른 경우
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    
    
    public void OnClickLobby()
    {
        ChangeState(UIState.Lobby);
    }

    public void OnClickJumag()
    {
        ChangeState(UIState.Jumag);
        jumagUI.npcWindow.SetActive(true);
        jumagUI.RandomJumagNPCScripts();
    }

    //public void OnClickHealStation()
    //{
    //    ChangeState(UIState.HealStation);
    //}

    public void OnClickSmith()
    {
        ChangeState(UIState.Smith);
    }

    public void OnClickTraining()
    {
        ChangeState(UIState.Training);
        trainingUI.npcTrainingWindow.SetActive(true);
        trainingUI.RandomJumagNPCScripts();
    }

    public void OnClickStartStage()
    {
        ChangeState(UIState.Stage);
    }

    public void OnClickCommonBattle()
    {
        ChangeState(UIState.CommonBattle);
    }

    public void OnClickTitle()
    {
        ChangeState(UIState.Title);
    }
    
    public void OnClickSettingButton()
    {
        settingPopupUI.gameObject.SetActive(true);
    }

    public void SetPlayer(string name)
    {
        characterSelectUI.SetPlayerCharacter(name);
    }

   

    public void SetCharacterInfo(Character character, int index)
    {
        trainingUI.SetCharacterStats(character);
        trainingUI.SelectCharacter(index);
    }

    public void AddPartnerSlotList(Character character)
    {
        partnerCharacters.Add(character);
        trainingUI.InstatiateCharacterSlotList(character);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(gold);
    }

    public void SpenGold(int amount)
    {
        gold -= amount;
        OnGoldChanged?.Invoke(gold);
    }

    public int GetGold() => gold;

    public Transform SetTraitTransform()
    {
        return trainingUI.traitTransform;
    }

    public Character SelectCharacter()
    {
        return trainingUI.selectCharacter;
    }

    public void ReturnCharacter(Character character)
    {
        jumagUI.allPartnerList.Add(character);
    }

    public void GeneratePartner()
    {
        jumagUI.GenerateNewPartners();
    }

    public void SetFriendlyPartyListView()
    {
        trainingUI.SetPartyListView();
    }



    public void SetCurrentStageName(SelectStageName currentStagaName, int currentNumber)
    {
        commonBattleUI.currentStageName = currentStagaName;
        commonBattleUI.currentStagenumber = currentNumber;
    }


    public void SetBattleCharacterOnData(Character character)
    {
        commonBattleUI.SetCharacterData(character);
        commonBattleUI.ShowUI(true);
    }

    public void OnEnableStageButton(SelectStageName stageName, int stageNumber)
    {
        switch (stageName)
        {
            case SelectStageName.Busan:
                stageSelectUI.busanStage.OnStageClear(stageNumber);
                if(stageSelectUI.busanStage.clearedStages.Contains(7))
                {
                    stageSelectUI.deaguButton.interactable = true;
                }
                break;
            case SelectStageName.Deagu:
                stageSelectUI.deaguStage.OnStageClear(stageNumber);
                if (stageSelectUI.deaguStage.clearedStages.Contains(7))
                {
                    stageSelectUI.deajonButton.interactable = true;
                }
                break;
            case SelectStageName.Deajon:
                stageSelectUI.deajonStage.OnStageClear(stageNumber);
                if (stageSelectUI.deajonStage.clearedStages.Contains(7))
                {
                    stageSelectUI.deajonButton.interactable = true;
                }
                break;
            case SelectStageName.Seoul:
                stageSelectUI.seoulStage.OnStageClear(stageNumber);
                break;
        }
    }

}
