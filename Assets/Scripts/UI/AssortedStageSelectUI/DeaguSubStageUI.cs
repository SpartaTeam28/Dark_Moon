using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeaguSubStageUI : BaseStageSelectUI
{
    protected override void InitializeStageConnections()
    {
        stageConnections = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2,3 } },       // 1번 스테이지 → 2,3,4번 스테이지 이동 가능
            { 2, new List<int> { 5 } },    // 2번 스테이지 → 5번 가능
            { 3, new List<int> { 4 } },       // 3번 스테이지 → 5,7번 가능
            { 4, new List<int> { 5 } },       // 4번 스테이지 → 6번 가능
            { 5, new List<int> { 6 } },    // 5번 스테이지 → 7번 가능
            { 6, new List<int> { 7 } }     // 6번 스테이지 → 7번 가능
        };

        // 첫 번째 스테이지는 항상 클리어된 상태로 설정
        clearedStages.Add(1);
    }



    public override void OpenButton()
    {
        gameObject.SetActive(true);
        UpdateStageButtons();
    }

    public override void CloseButton()
    {
        gameObject.SetActive(false);
    }

}
