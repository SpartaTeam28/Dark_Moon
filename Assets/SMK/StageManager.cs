using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{

    public static StageManager instance;
    public static StageManager Instance{ get { return instance; } }
    
    public int currentScene = 0; // 현재 스테이지
    
    int maxScene = 5; // 최대 스테이지

    public bool isMonsterDie = false; // 적이 다 죽으면 true로 바꿔줄것
    private bool bossSpawned = false; // 보스가 생성되었는지 체크

    public GameObject bossPrefab; // bossPrefab

    List<string> stage = new List<string> { "Busan", "Daegu", "Ulsan", "Daejeon", "Seoul" };
    
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Seoul" && !bossSpawned)
        {
            SpawnBoss();
        }
    }
    public void ChangeScene() // 스테이지 전환
    {
        if (isMonsterDie)
        {
            currentScene++;
            Debug.Log(currentScene);
            if (currentScene >= stage.Count)
            {
                CompleteStage();
                return;
            }
            isMonsterDie = false; // 다음 스테이지를 위해 초기화
            bossSpawned = false;  // 보스 상태 초기화

            SceneManager.LoadScene(stage[currentScene]);
            Debug.Log(stage[currentScene] + "로 이동합니다.");
        }
        else
        {
            RestartGame(); // 몬스터 다 안죽으면 MainScene으로 이동해야할 듯
           
        }

     
    }

    public void RestartGame()
    {
        currentScene = 0;
        isMonsterDie = false;
        bossSpawned = false;
      
        SceneManager.LoadScene("MainScene"); // 게임 재시작, 메인씬 정해지면 이동

    }

    public void CompleteStage()
    {
        
        Debug.Log("모든 씬을 클리어 했습니다");
        SceneManager.LoadScene("MainScene");
    }

    public void SpawnBoss() //보스 생성
    {
        if (!bossSpawned)
        {
            Instantiate(bossPrefab, new Vector3(5, 0, 0), Quaternion.identity);
            bossSpawned = true;
            Debug.Log("보스가 출현했습니다!");
        }
    }

    public void BossDefeated()
    {
        isMonsterDie = true; // 보스 사망 시 다음 스테이지 진행 가능
        Debug.Log("보스가 처치되었습니다. 엔딩으로 이동합니다");
    }
}

