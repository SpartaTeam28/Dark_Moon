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
            { 1, new List<int> { 2 } },       // 1�� �������� �� 2,3,4�� �������� �̵� ����
            { 2, new List<int> { 3 } },    // 2�� �������� �� 5�� ����
            { 3, new List<int> { 4,5,6 } },       // 3�� �������� �� 5,7�� ����
            { 4, new List<int> { 7 } },       // 4�� �������� �� 6�� ����
            { 5, new List<int> { 7 } },    // 5�� �������� �� 7�� ����
            { 6, new List<int> { 7 } }     // 6�� �������� �� 7�� ����
        };

        // ù ��° ���������� �׻� Ŭ����� ���·� ����
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
            SceneManager.LoadScene("���� �������� �̸�");
        }
        else
        {
            SceneManager.LoadScene("YGM_TestBattle");
        }

        UIManager.instance.OnClickCommonBattle();
    }
}
