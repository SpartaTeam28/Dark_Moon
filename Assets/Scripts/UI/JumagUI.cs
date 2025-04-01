
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
            // 영입된 동료 리스트
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
