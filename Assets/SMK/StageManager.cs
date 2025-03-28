using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
   
    public static StageManager Instance;
    int currentScene = 1;
    int maxScene = 5;
    bool isMonsterDie = false; // ���� �� ������ true�� �ٲ��ٰ�


    bool CheckDie()
    {
        // if monsterü�� ==0
        // ���ǹ� ���� �� monster�� �� �׾����� Ȯ�� �� 
        isMonsterDie = true;

        return isMonsterDie;
    }

    public void NextScene()
    {
        if (isMonsterDie)
        {
            currentScene++;
        }
        else
        {
            RestartGame(); // ���� �� �������� MainScene���� �̵��ؾ��� ��
        }

        if (currentScene > maxScene)
        {
            currentScene = maxScene;
            Debug.Log("��� ���� Ŭ���� �߽��ϴ�");
            //SceneManager.LoadScene("EndingScene"); // ��� �� Ŭ����� �������� �̵��ؾ��ϳ� ���
            return;
        }

        SceneManager.LoadScene("Stage"+currentScene);


    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene"); // ���� �����, ���ξ� �������� �̵�

    }
}
