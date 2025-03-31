using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeaguSubStageUI : BaseStageSelectUI
{
    protected override void InitializeStageConnections()
    {
        stageConnections = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2,3 } },       // 1�� �������� �� 2,3,4�� �������� �̵� ����
            { 2, new List<int> { 5 } },    // 2�� �������� �� 5�� ����
            { 3, new List<int> { 4 } },       // 3�� �������� �� 5,7�� ����
            { 4, new List<int> { 5 } },       // 4�� �������� �� 6�� ����
            { 5, new List<int> { 6 } },    // 5�� �������� �� 7�� ����
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

}
