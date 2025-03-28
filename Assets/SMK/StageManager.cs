using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{

    public static StageManager instance;
    public static StageManager Instance{ get { return instance; } }
    int currentScene = 1;
    int maxScene = 5;
    public bool isMonsterDie = false; // 적이 다 죽으면 true로 바꿔줄것

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    bool CheckDie()
    {
        // if monster체력 ==0
        // 조건문 넣을 것 monster가 다 죽었는지 확인 후 
        isMonsterDie = true;

        return isMonsterDie;
    }

    public void ChangeScene()
    {
        if (isMonsterDie)
        {
            currentScene++;
        }
        else
        {
            RestartGame(); // 몬스터 다 안죽으면 MainScene으로 이동해야할 듯
            return;
        }

        if (currentScene > maxScene)
        {
            currentScene = maxScene;
            Debug.Log("모든 씬을 클리어 했습니다");
            //SceneManager.LoadScene("EndingScene"); // 모든 씬 클리어시 메인으로 이동해야하나 고민
            return;
        }

        SceneManager.LoadScene("Stage"+currentScene);
        isMonsterDie=false; // 다음 스테이지를 위해 false로 변경


    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene"); // 게임 재시작, 메인씬 정해지면 이동

    }
}
