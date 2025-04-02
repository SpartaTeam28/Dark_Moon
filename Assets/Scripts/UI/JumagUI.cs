
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JumagUI : BaseUI
{
    public List<GameObject> buttonGameObjects;
    public Dictionary<int, Character> partnerSelectList = new Dictionary<int, Character>(); // �ָ��� ���� ����Ʈ
    public List<Character> allPartnerList; // ���� ������ ���� ����Ʈ

    public Button rerollButton;
    public Button exitButton;

    public TextMeshProUGUI goldText;

    public GameObject npcWindow;
    public TextMeshProUGUI npCScriptsText;
    public int reollGold;

    protected override UIState GetUIState()
    {
        return UIState.Jumag;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);


        for(int i = 0; i < buttonGameObjects.Count; i++)
        {
            int index = i;
            buttonGameObjects[i].GetComponent<Button>().onClick.AddListener(() => OnClickPartnerSelect(index));
        }

        rerollButton.onClick.AddListener(OnClickReroll);
        exitButton.onClick.AddListener(OnClickExitButton);

        GenerateNewPartners(); // �ʱ� ���� ����
    }

    private void OnEnable()
    {
        if(uiManager != null)
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


    public void GenerateNewPartners()
    {
        if (allPartnerList == null || allPartnerList.Count == 0)
        {
            return;
        }

        partnerSelectList.Clear();

        List<int> selectedIndexes = new List<int>(); // ���� ��ȣ ����

        int maxCount = Mathf.Min(4, allPartnerList.Count);  // �ִ� 4�����, ������ ���� ������ ���� �� ����

        while (selectedIndexes.Count < maxCount) // ���� ��ȣ �̱�
        {
            int randomIndex = Random.Range(0, allPartnerList.Count);

            // �ߺ� üũ
            if (!selectedIndexes.Contains(randomIndex))
            {
                selectedIndexes.Add(randomIndex);
            }
        }

        // ��ü ���� ����Ʈ���� �������� ���� ��ȣ UI�� ���� ����Ʈ �߰�
        for (int i = 0; i < selectedIndexes.Count; i++)
        {
            partnerSelectList[i] = allPartnerList[selectedIndexes[i]];
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        // ���� ������ UI�� �ݿ��ϴ� �ڵ�
        for (int i = 0; i < partnerSelectList.Count; i++)
        {
            // ���⿡ button ������Ʈ�� ���߿� �̹��� �ֱ�
            if (i < partnerSelectList.Count)
            {
                buttonGameObjects[i].gameObject.SetActive(true);
                buttonGameObjects[i].GetComponent<Image>().sprite = partnerSelectList[i].spriteRenderer.sprite;
            }
            else
            {
                buttonGameObjects[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnClickPartnerSelect(int index)
    {
        if (uiManager.partnerCharacters.Count >= uiManager.partnerMax)
        {
            uiManager.alarmPopupUI.gameObject.SetActive(true);
            uiManager.alarmPopupUI.ChangeAlarmText("�ִ� �ο����� �ʰ��߽��ϴ�.");
            return;
        }

        if (partnerSelectList.TryGetValue(index, out Character selectedPartner))
        {
            if (uiManager.gold < 100) return;
            uiManager.SpenGold(100);
            // ���Ե� ���� ����Ʈ
            //addList.Add(partnerSelectList[index]);
            uiManager.AddPartnerSlotList(partnerSelectList[index]);

            buttonGameObjects[index].gameObject.SetActive(false);

            allPartnerList.Remove(selectedPartner);

            partnerSelectList.Remove(index);
        }

    }


    public void OnClickReroll()
    {
        if (uiManager.gold < reollGold) return;
        uiManager.SpenGold(100);
        GenerateNewPartners();
    }

    public void OnClickExitButton()
    {
        uiManager.OnClickLobby();
    }

    public void OnClickCloseNpc()
    {
        npcWindow.SetActive(false);
    }

    public void RandomJumagNPCScripts()
    {
        int randomNuber = Random.Range(1,5);

        switch(randomNuber)
        {
            case 1:
                npCScriptsText.text = "������, � ���ñ���! �� �� ���ô��� ��� �����̼�. ��, �̸� ���� �����ñ���.";
                break;
            case 2:
                npCScriptsText.text = "� ���ð�! ���õ��� ������ �մ��� ���ϱ���. �� �帱��?";
                break;
            case 3:
                npCScriptsText.text = "�츮 �ָ� ������ �����̶��. �� �� ������ ���� �� ���� �ſ�.";
                break;
            case 4:
                npCScriptsText.text = "������ ���� �� �׸��̸� ������ �� ���� �ſ�. � ��ñ���.";
                break;
            case 5:
                npCScriptsText.text = "��� ���̼�? ���� �Ϸ� �̸� �� ���� ���̴���...";
                break;
        }
    }


}
