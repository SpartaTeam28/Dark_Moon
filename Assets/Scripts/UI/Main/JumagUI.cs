
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JumagUI : BaseUI
{
    public List<GameObject> buttonGameObjects;
    public Dictionary<int, Character> partnerSelectList = new Dictionary<int, Character>(); // 주막에 동료 리스트
    public List<Character> allPartnerList; // 구매 가능한 동료 리스트

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

        GenerateNewPartners(); // 초기 동료 생성
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

        List<int> selectedIndexes = new List<int>(); // 랜덤 번호 저장

        int maxCount = Mathf.Min(4, allPartnerList.Count);  // 최대 4명까지, 하지만 동료 수보다 많을 수 없음

        while (selectedIndexes.Count < maxCount) // 랜덤 번호 뽑기
        {
            int randomIndex = Random.Range(0, allPartnerList.Count);

            // 중복 체크
            if (!selectedIndexes.Contains(randomIndex))
            {
                selectedIndexes.Add(randomIndex);
            }
        }

        // 전체 동료 리스트에서 랜덤으로 뽑은 번호 UI에 뽑을 리스트 추가
        for (int i = 0; i < selectedIndexes.Count; i++)
        {
            partnerSelectList[i] = allPartnerList[selectedIndexes[i]];
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        // 동료 정보를 UI에 반영하는 코드
        for (int i = 0; i < partnerSelectList.Count; i++)
        {
            // 여기에 button 오브젝트에 나중에 이미지 넣기
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
            uiManager.alarmPopupUI.ChangeAlarmText("최대 인원수를 초과했습니다.");
            return;
        }

        if (partnerSelectList.TryGetValue(index, out Character selectedPartner))
        {
            if (uiManager.gold < 100) return;
            uiManager.SpenGold(100);
            // 영입된 동료 리스트
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
                npCScriptsText.text = "어이쿠, 어서 오시구려! 먼 길 오시느라 고생 많으셨소. 자, 이리 들어와 앉으시구려.";
                break;
            case 2:
                npCScriptsText.text = "어서 오시게! 오늘따라 유난히 손님이 귀하구려. 뭘 드릴까?";
                break;
            case 3:
                npCScriptsText.text = "우리 주막 술맛은 으뜸이라오. 한 번 맛보면 잊을 수 없을 거요.";
                break;
            case 4:
                npCScriptsText.text = "따뜻한 국밥 한 그릇이면 추위가 싹 가실 거요. 어서 드시구려.";
                break;
            case 5:
                npCScriptsText.text = "어디서 오셨소? 무슨 일로 이리 먼 길을 오셨는지...";
                break;
        }
    }


}
