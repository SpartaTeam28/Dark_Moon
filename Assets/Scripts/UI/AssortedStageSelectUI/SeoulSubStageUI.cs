using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeoulSubStageUI : BaseStageSelectUI
{

    protected override void Awake()
    {
        base.Awake();
        currentStageName = SelectStageName.Seoul;

        for (int i = 0; i < subStageButtonList.Count; i++)
        {
            int index = i;
            subStageButtonList[i].onClick.AddListener(() => OnClickStageButton(index + 1));
        }
    }

    protected override void InitializeStageConnections()
    {
        stageConnections = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2 } },       // 1번 스테이지 → 2,3,4번 스테이지 이동 가능
            { 2, new List<int> { 3 } },    // 2번 스테이지 → 5번 가능
            { 3, new List<int> { 4,5,6 } },       // 3번 스테이지 → 5,7번 가능
            { 4, new List<int> { 7 } },       // 4번 스테이지 → 6번 가능
            { 5, new List<int> { 7 } },    // 5번 스테이지 → 7번 가능
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

    public override void OnClickStageButton(int stagenumeber)
    {
        UIManager.instance.SetCurrentStageName(currentStageName, stagenumeber);
        if (stagenumeber == 7)
        {
            SceneManager.LoadScene("메인 스테이지 이름");
        }
        else
        {
            SceneManager.LoadScene("YGM_TestBattle");
        }

        UIManager.instance.OnClickCommonBattle();
    }
}
