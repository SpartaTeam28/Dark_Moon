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
    private List<bool> unlockedStages = new List<bool>(); // 스테이지 해금 여부
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            LoadUnlockData(); // 저장된 스테이지 해금 정보 불러오기
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);

        }
       
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadUnlockData(); // 씬 변경 후 스테이지 해금 정보 다시 불러오기

        if (scene.name == "Seoul" && !bossSpawned)
        {
            SpawnBoss();
        }
    }
    public void ChangeScene()
    {
        if (isMonsterDie)
        {
            if (currentScene + 1 < stage.Count)
            {
                UnlockStage(currentScene + 1); // 다음 스테이지 해금
                SaveUnlockData();
                
                currentScene++;
                isMonsterDie = false;
                bossSpawned = false;
                SceneManager.LoadScene(stage[currentScene]);
                Debug.Log(stage[currentScene] + "로 이동합니다.");
            }
            else
            {
                CompleteStage();
            }
        }
        else
        {
            RestartGame();
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
        SceneManager.LoadScene("MainScene"); // 성공씬
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

    private void LoadUnlockData()
    {
        unlockedStages.Clear();
        for (int i = 0; i < stage.Count; i++)
        {
            unlockedStages.Add(PlayerPrefs.GetInt("StageUnlocked" + i, i == 0 ? 1 : 0) == 1);
        }
    }

    private void SaveUnlockData()
    {
        for (int i = 0; i < unlockedStages.Count; i++)
        {
            PlayerPrefs.SetInt("StageUnlocked" + i, unlockedStages[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void UnlockStage(int stageIndex)
    {
        if (stageIndex > 0 && !unlockedStages[stageIndex - 1])
        {
            Debug.LogError("이전 스테이지를 클리어해야 합니다!");
            return;
        }

        if (stageIndex < unlockedStages.Count && !unlockedStages[stageIndex])
        {
            unlockedStages[stageIndex] = true;
            SaveUnlockData();
            Debug.Log(stage[stageIndex] + " 스테이지가 해금되었습니다!");
        }
    }
}

