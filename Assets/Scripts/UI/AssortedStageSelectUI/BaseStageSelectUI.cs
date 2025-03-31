using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseStageSelectUI : MonoBehaviour
{
    public List<Button> subStageButtonList;
    public Button closeButton;

    public Dictionary<int, Button> subStageRoutButton = new Dictionary<int, Button>();

    public Dictionary<int, List<int>> stageConnections; // 각 스테이지가 이동할 수 있는 스테이지 목록
    public HashSet<int> clearedStages = new HashSet<int>(); // 클리어한 스테이지 목록
    public int currentStage = 1; // 현재 스테이지 (예제)

    protected virtual void Awake()
    {
        for (int i = 0; i < subStageButtonList.Count; i++)
        {
            subStageRoutButton[i + 1] = subStageButtonList[i];
        }

        for(int i = 0; i < subStageButtonList.Count; i++)
        {

        }

        InitializeStageConnections(); // 스테이지 연결 정보 설정
        UpdateStageButtons(); // 초기 버튼 상태 업데이트

    }



    protected abstract void InitializeStageConnections();
    //{
        //stageConnections = new Dictionary<int, List<int>>
        //{
        //    { 1, new List<int> { 2,3,4 } },       // 1번 스테이지 → 2,3,4번 스테이지 이동 가능
        //    { 2, new List<int> { 5 } },    // 2번 스테이지 → 5번 가능
        //    { 3, new List<int> { 5, 7 } },       // 3번 스테이지 → 5,7번 가능
        //    { 4, new List<int> { 6 } },       // 4번 스테이지 → 6번 가능
        //    { 5, new List<int> { 7 } },    // 5번 스테이지 → 7번 가능
        //    { 6, new List<int> { 7 } }     // 6번 스테이지 → 7번 가능
        //};

        //// 첫 번째 스테이지는 항상 클리어된 상태로 설정
        //clearedStages.Add(1);
    //}

    protected void UpdateStageButtons()
    {
        foreach (var buttonPair in subStageRoutButton)
        {
            int stageNumber = buttonPair.Key;
            Button stageButton = buttonPair.Value;

            if (clearedStages.Contains(stageNumber) || IsConnectedAndUnlocked(stageNumber))
            {
                stageButton.interactable = true; // 활성화
            }
            else
            {
                stageButton.interactable = false; // 비활성화
            }
        }
    }

    protected bool IsConnectedAndUnlocked(int stage)
    {
        foreach (var clearedStage in clearedStages)
        {
            if (stageConnections.ContainsKey(clearedStage) && stageConnections[clearedStage].Contains(stage))
            {
                return true; // 클리어한 스테이지에서 이동 가능하면 활성화
            }
        }
        return false;
    }


    public void OnStageClear(int stageNumber)
    {
        if (!clearedStages.Contains(stageNumber))
        {
            clearedStages.Add(stageNumber);
            UpdateStageButtons(); // 버튼 상태 업데이트
        }
    }

   


    public abstract void OpenButton();


    public abstract void CloseButton();


}
