
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
            }
            else
            {
                buttonGameObjects[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnClickPartnerSelect(int index)
    {
        if (partnerSelectList.TryGetValue(index, out Character selectedPartner))
        {
            // ���Ե� ���� ����Ʈ
            //addList.Add(partnerSelectList[index]);
            uiManager.AddPartnerSlotList(partnerSelectList[index]);

            buttonGameObjects[index].gameObject.SetActive(false);
            uiManager.SpenGold(100);

            allPartnerList.Remove(selectedPartner);

            partnerSelectList.Remove(index);
        }

    }


    public void OnClickReroll()
    {
        GenerateNewPartners();
    }

    public void OnClickExitButton()
    {
        uiManager.OnClickLobby();
    }


}
