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
        if (selectCharacter == null) return; // ���� ����
        if (GameManager.instance.friendlyCharacterList.Count >= 4) return;
        if (GameManager.instance.friendlyCharacterList.Contains(selectCharacter)) return; // �ߺ� ����
        GameManager.instance.friendlyCharacterList.Add(selectCharacter);
        SetPartyListView();
        
        
    }


    // �̹��� ������Ʈ
    /**
    private void UpdatePartyUI()
    {
        for (int i = 0; i < BattleUnitImage.Length; i++)
        {
            if (i < GameManager.instance.friendlyCharacterList.Count)
            {
                BattleUnitImage[i].sprite = GameManager.instance.friendlyCharacterList[i].characterSprite;
                BattleUnitImage[i].enabled = true; // �̹��� Ȱ��ȭ
            }
            else
            {
                BattleUnitImage[i].sprite = null; // �� ����
                BattleUnitImage[i].enabled = false; // ��Ȱ��ȭ
            }
        }
    }
    **/


    public void OnClickRemoveButton()
    {
        if (selectCharacter == null) return; // ���� ����
        if (GameManager.instance.friendlyCharacterList.Count <= 1) return; // �ּ� 1�� ����
        if (!GameManager.instance.friendlyCharacterList.Contains(selectCharacter)) return; // ���� ĳ���� ���� ����
        GameManager.instance.friendlyCharacterList.Remove(selectCharacter);

        SetPartyListView();
        ResetView();
    }

    public void OnClickReRollButton()
    {
        characterSlotList[selectCharacterIndex].RerollTrait();
    }

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
    }

    public void OnClickDeleteButton()
    {
        DeleteCharacter();
    }

    public void DeleteCharacter()
    {
        if (selectCharacter == null) return;

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
            Debug.LogError("��ȿ���� ���� �ε��� ����: " + index);
            return;
        }

        selectCharacter = characterSlotList[index].character;
        selectCharacterIndex = index;
    }


    public void ResetView()
    {
        selectCharacter = null;
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


        for (int i = 0; i < BattleUnitImage.Count; i++)
        {
            BattleUnitImage[i].sprite = GameManager.instance.friendlyCharacterList[i].icon;
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
                npcTrainingScriptsText.text = "���� �ȹٷ� ������! �Ʒ��� ����ó��! ������ �Ʒ�ó��!";
                break;
            case 2:
                npcTrainingScriptsText.text = "������ �����ϱ���! �ٽ� �ѹ�! ������ ���� ���Ķ�!";
                break;
            case 3:
                npcTrainingScriptsText.text = "������ �ܼ��� �������� �Ǵ� ���� �ƴϴ�. ������ ���� �۾� ������ ���� Ű���� �Ѵ�.";
                break;
            case 4:
                npcTrainingScriptsText.text = "������ ������ ���� �����ϴ� ���� �ƴ϶�, �ڽ��� �ٽ����� ���̴�.";
                break;
            case 5:
                npcTrainingScriptsText.text = "���� ������ �����, ���ڸ� ��ȣ�ϴ� ������ ���� ����.";
                break;
        }
    }



}
