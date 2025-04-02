using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Battle_Silhum : MonoBehaviour
{
    //

    private static Battle_Silhum instance;
    public static Battle_Silhum Instance {  get { return instance; }  set { instance = value; } }
    public Turn turn; // 턴 상태
    public bool isAlive; // 살아있는가
    public Transform buttonPanel; // 버튼들이 위치할 UI 패널
    public GameObject attackButtonPrefab; // 버튼 프리팹 (Inspector에서 설정
    public bool isLock; // 잠금
    public int TurnCount = 0;

    
    public List<Character> players;
    public List<Character> enemies;  // 적 리스트
    public List<Character> turnOrder; // 턴 순서 리스트
    int currentTurnIndex = 0; // 현재 턴 진행 중인 캐릭터 인덱스

    private List<Button> attackButtons = new List<Button>(); // 버튼 리스트

    [Header("스테이지별 적 리스트")]
    [SerializeField] public List<Character> UlsanEnemies;
    [SerializeField] private List<Character> BusanEnemies;
    [SerializeField] private List<Character> HanbatEnemies;
    [SerializeField] private List<Character> DeaguEnemies;
    [SerializeField] private List<Character> BattleScene;
    [SerializeField] private int playerDeathCount;
    [SerializeField] private int enemyDeathCount;


    public Transform speedTextPanel; // UI에서 Speed 값을 표시할 Panel
    public GameObject speedTextPrefab; // Speed 값을 표시할 Text 프리팹
    private StageWeather StageWeather;
    private Dictionary<Character, TextMeshProUGUI> speedTexts = new Dictionary<Character, TextMeshProUGUI>();
    public TextMeshProUGUI cleartText;
    public Button button;

    int equipGold = 500;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadEnemies();
        turn = Turn.start; // 전투 시작

    }
    private void Start()
    {
        BattleStart();
    }
    public void BattleStart()
    {
        // 스피드 비교해서 턴 정하기
        SpeedCheck();
        //CreateAttackButtons();
       
        ClickManager.Instance.next = NextTurn;
        playerDeathCount = GameManager.instance.friendlyCharacterList.Count;
        Debug.Log("Count"+playerDeathCount);
        enemyDeathCount = (int)(GameManager.instance.EnemyCharacterList?.Count);
        NextTurn();
        StageWeather = FindAnyObjectByType<StageWeather>();
        StageWeather.WeatherRandomStart();

    }
    public void LoadEnemies()
    {
        // 스테이지에 맞는 적 리스트 로드
        Debug.Log($"UlsanEnemies Count: {DeaguEnemies.Count}"); // 몇 개의 적이 있는지 확인

    //    string currentScene = SceneManager.GetActiveScene().name;

    //    switch (currentScene)
    //    {
    //        case "Ulsan":
    //            enemies = new List<Character>(UlsanEnemies);
    //            Debug.Log($"UlsanEnemies Count: {UlsanEnemies.Count}");
    //            break;

    //        case "Daegu":
    //            enemies = new List<Character>(DeaguEnemies);
    //            Debug.Log($"DeaguEnemies Count: {DeaguEnemies.Count}");
    //            break;

    //        case "Busan":
    //            enemies = new List<Character>(BusanEnemies);
    //            Debug.Log($"BusanEnemies Count: {BusanEnemies.Count}");
    //            break;

    //        case "Hanbat":
    //            enemies = new List<Character>(HanbatEnemies);
    //            Debug.Log($"HanbatEnemies Count: {HanbatEnemies.Count}");
    //            break;
    //        case "Battle_Scene":
    //            enemies = new List<Character>(BattleScene);
    //            Debug.Log($"HanbatEnemies Count: {BattleScene.Count}");
    //            break;
    //        default:
    //            Debug.LogError($"�� ����Ʈ�� �����ϴ�! ���� ��: {currentScene}");
    //            break;
    //    }
    //    Debug.Log($"Enemies Count after LoadEnemies(): {enemies.Count}"); // enemies�� ���������� �����ƴ��� Ȯ��
    }

    public void SpeedCheck()
    {
        // 플레이어와 적 리스트를 하나의 리스트로 합친 후 속도 순 정렬
        turnOrder = new List<Character>();
        turnOrder.AddRange(players);
        turnOrder.AddRange(enemies);
        // 수정? 고민
        turnOrder = turnOrder.OrderByDescending(c => c.stat.speed.value).ToList();
        currentTurnIndex = 0; // 첫 번째 캐릭터부터 시작

        Debug.Log("=== 캐릭터 Speed 순서 ===");
        foreach (var character in turnOrder)
        {
            Debug.Log($" Speed {character.stat.speed.value}");
        }
        DisplaySpeedTexts();
    }
    public void NextTurn()
    {
        TurnCount++;
        EndGameTrigger();
        if (!isAlive)
        {
            turn = Turn.win;
            EndBattle();
            return;
        }
        WeatherChange();

        Debug.Log("Turn Change");
        if(currentTurnIndex == turnOrder.Count)
        {
            currentTurnIndex = 0;
        }
        Character currentCharacter = turnOrder[currentTurnIndex];

        currentTurnIndex++;
                
        Debug.Log(currentTurnIndex.ToString() + "지금 인덱스");

        if (players.Contains(currentCharacter))
        {
            turn = Turn.playerTurn;
            ClickManager.Instance.SetSkillBook(currentCharacter.GetComponent<SkillBook>());
            ClickManager.Instance.SetCharecterStat(currentCharacter.stat);
            currentCharacter.StartTurn();
        }
        else
        {
            turn = Turn.enemyTurn;
            ClickManager.Instance.SetSkillBook(null);
            ClickManager.Instance.SetCharecterStat(currentCharacter.GetComponent<CharacterStat>());
            UpdateButtonState(false);
            currentCharacter.StartTurn();
            StartCoroutine(EnemyAttack(currentCharacter));
        }
    }
    IEnumerator EnemyAttack(Character enemy)
    {
        yield return new WaitForSeconds(2f);
        enemy.GetComponent<Enemy>().
            SkillActive(enemy.GetComponent<Enemy>().sKilldatas
            [Random.Range(0, enemy.GetComponent<Enemy>().sKilldatas.Length)]);
        Debug.Log($"{enemy.name}이(가) 공격을 했습니다.");
        yield return new WaitForSeconds(2f);
        ClickManager.Instance.TargetDown();
        NextTurn();
    }
    //private void CreateAttackButtons()
    //{
    //    foreach (var btn in attackButtons)
    //    {
    //        Destroy(btn.gameObject); // 기존 버튼 삭제
    //    }
    //    attackButtons.Clear();

    //    for (int i = 0; i < players.Count; i++)
    //    {
    //        GameObject newButton = Instantiate(attackButtonPrefab, buttonPanel);
    //        newButton.GetComponentInChildren<Text>().text = players[i].name;
    //        int index = i; // 람다 캡처 방지
    //        newButton.GetComponent<Button>().onClick.AddListener(() => PlayerAttack(index));
    //        attackButtons.Add(newButton.GetComponent<Button>());
    //    }
    //}
    public void EndBattle()
    {
        Debug.Log("전투 끝");
    }

    private void UpdateButtonState(bool active)
    {
        foreach (var btn in attackButtons)
        {
            btn.interactable = active;
        }
    }



    public void PlayerDie(Character character)
    {
        if(character.CompareTag("Friends"))
        {
            playerDeathCount--;
        }
        else if(character.CompareTag("Enemy"))
        {
            enemyDeathCount--;
        }


        turnOrder.Remove(character);
        character.gameObject.SetActive(false);
    }
    public void EndGameTrigger()
    {

        if (playerDeathCount == 0)
        {
            turn = Turn.lose;
            // //StartCoroutine(LoseGame());
            UIManager.instance.SetBattlePanel();
            //cleartText.text = $"전투에서 패배하였습니다";
            return;
        }
        if (enemyDeathCount == 0)
        {
            isAlive = false;
            turn = Turn.win;
            for (int i = 0; i < GameManager.Instance.friendlyCharacterList.Count; i++)
            {
                GameManager.Instance.friendlyCharacterList[i].info.AddExp(15);
            }
            UIManager.instance.AddGold(500);
            
           
            Debug.Log("호출");
            UIManager.instance.SetBattlePanel();
           // cleartText.text = $"축하합니다. 전투에서 승리하셨습니다! 경험치 15와 Gold {equipGold}을 얻으셨습니다!";
            return;
        }
        equipGold += 500;
      
    }


    private void DisplaySpeedTexts()
    {
        int i = 1;
        foreach (var pair in speedTexts)
        {
            Destroy(pair.Value.gameObject); // 기존 UI 삭제
        }
        speedTexts.Clear();

        foreach (var character in turnOrder)
        {

           // GameObject newText = Instantiate(speedTextPrefab, speedTextPanel);
            //TextMeshProUGUI textComponent = newText.GetComponent<TextMeshProUGUI>();
            //textComponent.text = $" Speed: {character.stat.speed.value}";
            //textComponent.text = $" Speed: {i}";
            Vector3 screenPos = Camera.main.WorldToScreenPoint(character.transform.position);
            //newText.transform.position = screenPos + new Vector3(0, 100f, 0); // Y값 조정 (아래로 내리기)

           // speedTexts[character] = textComponent;
            i++;
        }
    }
    public IEnumerator ClearGame()
    {
        yield return new WaitForSeconds(2f);
        CommonBattleUI.instance.OnClickWinButton();
    }

    public IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(2f);
        CommonBattleUI.instance.OnClickDefeatButton();
    }

    public void WeatherChange()
    {
        if(TurnCount % 5 == 0)
        {
            StageWeather.WeatherChange();
        }
    }

    public void OnSceneChange()
    {
        SceneManager.LoadScene("YGM_Scene");
        
;   }
}
