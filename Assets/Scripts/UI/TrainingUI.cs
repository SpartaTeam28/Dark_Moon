using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class TrainingUI : BaseUI
{
    public Button setButton;
    public Button removePartyButton;
    public Button deleteButton;
    public Button lobbyButton;
    public Button rerollTraitButton;

    public CharacterSlot characterSlotPrefab;
    public List<CharacterSlot> characterSlotList;

    public List<Image> BattleUnitImage;
    public Sprite nullImageSprite;
    public Image selectImageCharacter;

    public Transform contentGamobject;
    public Transform traitTransform;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenceText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI criticalText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI evasionText;
    public TextMeshProUGUI expText;

    public TextMeshProUGUI goldText;
    public int traitrerollGold;
    public Character selectCharacter;
    int selectCharacterIndex;


    public GameObject npcTrainingWindow;
    public TextMeshProUGUI npcTrainingScriptsText;

    protected override UIState GetUIState()
    {
        return UIState.Training;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        setButton.onClick.AddListener(OnClickSetButton);
        removePartyButton.onClick.AddListener(OnClickRemoveButton);
        lobbyButton.onClick.AddListener(OnClicklobbyButton);
        deleteButton.onClick.AddListener(OnClickDeleteButton);
        rerollTraitButton.onClick.AddListener(OnClickReRollButton);
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
        if (uiManager != null)
        {
            uiManager.OnGoldChanged -= UpdateGoldUI;
        }

    }

    private void UpdateGoldUI(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void OnClickSetButton()
    {
        if (selectCharacter == null) return; // 예외 방지
        if (GameManager.instance.friendlyCharacterList.Count >= 4) return;
        if (GameManager.instance.friendlyCharacterList.Contains(selectCharacter)) return; // 중복 방지
        GameManager.instance.friendlyCharacterList.Add(selectCharacter);

        for(int i = 0; i < characterSlotList.Count; i++)
        {
            if (characterSlotList[i].character == selectCharacter)
            {
                characterSlotList[i].Onparty.SetActive(true);
            }
        }

        SetPartyListView();
       
    } // 파티 추가

    public void OnClickRemoveButton()
    {
        if (selectCharacter == null) return; // 예외 방지
        if (GameManager.instance.friendlyCharacterList.Count <= 1) return; // 최소 1명 유지
        if (!GameManager.instance.friendlyCharacterList.Contains(selectCharacter)) return; // 없는 캐릭터 삭제 방지
        GameManager.instance.friendlyCharacterList.Remove(selectCharacter);

        for(int i = 0; i < characterSlotList.Count; i++)
        {
            if (characterSlotList[i].character == selectCharacter)
            {
                characterSlotList[i].Onparty.SetActive(false);
            }

            characterSlotList[i].selectImage.SetActive(false);
        }
        
        SetPartyListView();
        ResetView();
    } // 파티 제거

    public void OnClickReRollButton()
    {
        uiManager.SpenGold(traitrerollGold);
        if (uiManager.gold < traitrerollGold) return;

        characterSlotList[selectCharacterIndex].RerollTrait();
    }  // 특성 리롤

    public void OnClicklobbyButton()
    {
        uiManager.OnClickLobby();
        ResetView();
    }

    public void InstatiateCharacterSlotList(Character character)
    {
        CharacterSlot newCharacterSlot = Instantiate(characterSlotPrefab, contentGamobject);
        newCharacterSlot.character = character;
        newCharacterSlot.SetNameText();
        newCharacterSlot.SetJobText();
        newCharacterSlot.SetLvText();
        newCharacterSlot.SetCharacterImage();
        characterSlotList.Add(newCharacterSlot);

        for(int i = 0; i < characterSlotList.Count; i++)
        {
            characterSlotList[i].slotIndex = i;
        }
    } // UI에 캐릭터 슬롯 생성

    public void OnClickDeleteButton()
    {
        DeleteCharacter();
    } // 캐릭터 슬롯 제거

    public void DeleteCharacter()
    {
        if (selectCharacter == null) return;

        for(int i = 0; i < GameManager.instance.friendlyCharacterList.Count; i++)
        {
            if (GameManager.instance.friendlyCharacterList[i] == selectCharacter)
            {
                Debug.Log("파티를 해제해주세요.");
                return;
            }

        }

        if(selectCharacterIndex != 0)
        {
            Transform selectedCharacter = contentGamobject.GetChild(selectCharacterIndex);

            if (selectedCharacter != null)
            {
                Destroy(selectedCharacter.gameObject);
            }

            uiManager.ReturnCharacter(selectCharacter);
            uiManager.partnerCharacters.RemoveAt(selectCharacterIndex);
            characterSlotList.RemoveAt(selectCharacterIndex);
            ResetView();
            selectCharacter = null;
            selectCharacterIndex = 0;

            for (int i = 0; i < characterSlotList.Count; i++)
            {
                characterSlotList[i].slotIndex = i;
            }
        }
    }


    public void SetCharacterStats(Character character)
    {
        for (int i = 0; i < characterSlotList.Count; i++)
        {
            if (characterSlotList[i].character == character)
            {
                characterSlotList[i].selectImage.SetActive(true);
            }
            else
            {
                characterSlotList[i].selectImage.SetActive(false);
            }

        }

        selectImageCharacter.sprite = character.icon;
        attackText.text = character.stat.attack.GetValueToString();
        defenceText.text = character.stat.defence.GetValueToString();
        healthText.text = character.stat.health.GetValueToString();
        manaText.text = character.stat.mana.GetValueToString();
        criticalText.text = character.stat.critical.GetValueToString();
        accuracyText.text = character.stat.accuracy.GetValueToString();
        evasionText.text = character.stat.accuracy.GetValueToString();
        expText.text = $"{character.info.curExp} / {character.info.totalExp}";
    }

    public void SelectCharacter(int index)
    {
        if (index < 0 || index >= characterSlotList.Count)
        {
            Debug.LogError("유효하지 않은 인덱스 선택: " + index);
            return;
        }

        selectCharacter = characterSlotList[index].character;
        selectCharacterIndex = index;
    }


    public void ResetView()
    {
        selectCharacter = null;
        selectImageCharacter.sprite = nullImageSprite;
        attackText.text = "";
        defenceText.text = "";
        healthText.text = "";
        manaText.text = "";
        criticalText.text = "";
        accuracyText.text = "";
        evasionText.text = "";

        foreach(Transform child in traitTransform)
        {
            Destroy(child.gameObject);
        }

    }

    public void SetPartyListView()
    {
        for (int i = 0; i < BattleUnitImage.Count; i++)
        {
            BattleUnitImage[i].sprite = nullImageSprite;
        }


        for (int i = 0; i < GameManager.instance.friendlyCharacterList.Count; i++)
        {
            BattleUnitImage[i].sprite = GameManager.instance.friendlyCharacterList[i].icon;
        }
    }

    public void UpdateTrainingSlotUI()
    {
        for (int i = 0; i < characterSlotList.Count; i++)
        {
            characterSlotList[i].SetNameText();
            characterSlotList[i].SetJobText();
            characterSlotList[i].SetLvText();
        }     
    }


    public void OnClickCloseNpc()
    {
        npcTrainingWindow.SetActive(false);
    }

    public void RandomJumagNPCScripts()
    {
        int randomNuber = Random.Range(1, 5);

        switch (randomNuber)
        {
            case 1:
                npcTrainingScriptsText.text = "정신 똑바로 차리고! 훈련은 실전처럼! 실전은 훈련처럼!";
                break;
            case 2:
                npcTrainingScriptsText.text = "기합이 부족하구나! 다시 한번! 전력을 다해 외쳐라!";
                break;
            case 3:
                npcTrainingScriptsText.text = "무예는 단순히 힘만으로 되는 것이 아니다. 정신을 갈고 닦아 내면의 힘을 키워야 한다.";
                break;
            case 4:
                npcTrainingScriptsText.text = "진정한 강함은 남을 지배하는 것이 아니라, 자신을 다스리는 것이다.";
                break;
            case 5:
                npcTrainingScriptsText.text = "명예를 소중히 여기고, 약자를 보호하는 마음을 잊지 마라.";
                break;
        }
    }



}
