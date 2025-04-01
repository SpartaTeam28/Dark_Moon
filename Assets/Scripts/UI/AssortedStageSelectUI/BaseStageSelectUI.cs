using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseStageSelectUI : MonoBehaviour
{
    public List<Button> subStageButtonList;
    public Button closeButton;

    public Dictionary<int, Button> subStageRoutButton = new Dictionary<int, Button>();

    public Dictionary<int, List<int>> stageConnections; // �� ���������� �̵��� �� �ִ� �������� ���
    public HashSet<int> clearedStages = new HashSet<int>(); // Ŭ������ �������� ���
    public int currentStage = 1; // ���� �������� (����)

    protected virtual void Awake()
    {
        for (int i = 0; i < subStageButtonList.Count; i++)
        {
            subStageRoutButton[i + 1] = subStageButtonList[i];
        }

        for(int i = 0; i < subStageButtonList.Count; i++)
        {

        }

        InitializeStageConnections(); // �������� ���� ���� ����
        UpdateStageButtons(); // �ʱ� ��ư ���� ������Ʈ

    }



    protected abstract void InitializeStageConnections();
    //{
        //stageConnections = new Dictionary<int, List<int>>
        //{
        //    { 1, new List<int> { 2,3,4 } },       // 1�� �������� �� 2,3,4�� �������� �̵� ����
        //    { 2, new List<int> { 5 } },    // 2�� �������� �� 5�� ����
        //    { 3, new List<int> { 5, 7 } },       // 3�� �������� �� 5,7�� ����
        //    { 4, new List<int> { 6 } },       // 4�� �������� �� 6�� ����
        //    { 5, new List<int> { 7 } },    // 5�� �������� �� 7�� ����
        //    { 6, new List<int> { 7 } }     // 6�� �������� �� 7�� ����
        //};

        //// ù ��° ���������� �׻� Ŭ����� ���·� ����
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
                stageButton.interactable = true; // Ȱ��ȭ
            }
            else
            {
                stageButton.interactable = false; // ��Ȱ��ȭ
            }
        }
    }

    protected bool IsConnectedAndUnlocked(int stage)
    {
        foreach (var clearedStage in clearedStages)
        {
            if (stageConnections.ContainsKey(clearedStage) && stageConnections[clearedStage].Contains(stage))
            {
                return true; // Ŭ������ ������������ �̵� �����ϸ� Ȱ��ȭ
            }
        }
        return false;
    }


    public void OnStageClear(int stageNumber)
    {
        if (!clearedStages.Contains(stageNumber))
        {
            clearedStages.Add(stageNumber);
            UpdateStageButtons(); // ��ư ���� ������Ʈ
        }
    }

   


    public abstract void OpenButton();


    public abstract void CloseButton();


}
