using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;


public enum StartJobSelect
{
    JobOne,
    JobTwo,
    JobThree
}


public class CharacterSelectUI : BaseUI
{
    public Button firstSelect;
    public Button secondSelect;
    public Button thirdSelect;
    public Button nexButton;
    public Button backButton;
    public NickNamePopupUI nickNamePopup;
    public AlamPopupUI alarmPopup;

    public List<Character> startCharacter;

    public Outline jobOne;
    public Outline jobTwo;
    public Outline jobThree;

    public StartJobSelect? currentSelection = null;
    protected override UIState GetUIState()
    {
        return UIState.CharacterSelect;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        firstSelect.onClick.AddListener(OnClickSelectOne);
        secondSelect.onClick.AddListener(OnClickSelectTwo);
        thirdSelect.onClick.AddListener(OnClickSelectThree);
        nexButton.onClick.AddListener(OnClickCharacterSelect);
        backButton.onClick.AddListener(OnClickBack);
    }

    public void OnClickSelectOne()
    {
        SelectJob(StartJobSelect.JobOne);
    }

    public void OnClickSelectTwo()
    {
        SelectJob(StartJobSelect.JobTwo);
    }

    public void OnClickSelectThree()
    {
        SelectJob(StartJobSelect.JobThree);
    }

    public void OnClickBack()
    {
        uiManager.OnClickTitle();
        jobOne.enabled = false;
        jobTwo.enabled = false;
        jobThree.enabled = false;
        currentSelection = null;

    }

    public void OnClickCharacterSelect()
    {
        if (currentSelection == null)
        {
            alarmPopup.gameObject.SetActive(true);
            return;
        }

        nickNamePopup.gameObject.SetActive(true);
    }



    private void SelectJob(StartJobSelect job)
    {
        // 선택된 직업 저장
        currentSelection = job;

        // 모든 Outline 비활성화
        jobOne.enabled = false;
        jobTwo.enabled = false;
        jobThree.enabled = false;

        // 선택된 Outline만 활성화
        switch (job)
        {
            case StartJobSelect.JobOne:
                jobOne.enabled = true;
                break;
            case StartJobSelect.JobTwo:
                jobTwo.enabled = true;
                break;
            case StartJobSelect.JobThree:
                jobThree.enabled = true;
                break;
        }

    }

    public void SetPlayerCharacter(string name)
    {
        int index = (int)currentSelection; // Enum을 int로 변환
        Character selectedCharacter = startCharacter[index];
        selectedCharacter.info.characterName = name;
        GameManager.instance.friendlyCharacterList.Add(selectedCharacter);
        uiManager.AddPartnerSlotList(selectedCharacter);
    }
}
