using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Battle_Silhum : MonoBehaviour
{
    //
    public Turn turn; // 턴 상태
    public bool isAlive; // 살아있는가
    public Transform buttonPanel; // 버튼들이 위치할 UI 패널
    public GameObject attackButtonPrefab; // 버튼 프리팹 (Inspector에서 설정
    public bool isLock; // 잠금

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

    private void Awake()
    {
        //players = new List<Character>(GameManager.Instance.friendlyCharacterList);
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
        NextTurn();
    }
    public void LoadEnemies()
    {
        // 스테이지에 맞는 적 리스트 로드
        Debug.Log($"UlsanEnemies Count: {DeaguEnemies.Count}"); // 몇 개의 적이 있는지 확인

        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Ulsan":
                enemies = new List<Character>(UlsanEnemies);
                Debug.Log($"UlsanEnemies Count: {UlsanEnemies.Count}");
                break;

            case "Daegu":
                enemies = new List<Character>(DeaguEnemies);
                Debug.Log($"DeaguEnemies Count: {DeaguEnemies.Count}");
                break;

            case "Busan":
                enemies = new List<Character>(BusanEnemies);
                Debug.Log($"BusanEnemies Count: {BusanEnemies.Count}");
                break;

            case "Hanbat":
                enemies = new List<Character>(HanbatEnemies);
                Debug.Log($"HanbatEnemies Count: {HanbatEnemies.Count}");
                break;

            default:
                Debug.LogError($"적 리스트가 없습니다! 현재 씬: {currentScene}");
                break;
        }
        Debug.Log($"Enemies Count after LoadEnemies(): {enemies.Count}"); // enemies가 정상적으로 설정됐는지 확인
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
    }
    public void NextTurn()
    {
        if (!isAlive)
        {
            turn = Turn.win;
            EndBattle();
            return;
        }

        Character currentCharacter = turnOrder[currentTurnIndex];

        if (players.Contains(currentCharacter))
        {
            turn = Turn.playerTurn;
            Debug.Log($"{currentCharacter.name}의 턴입니다. 공격 버튼을 눌러 공격하세요.");
            UpdateButtonState(true);
        }
        else
        {
            turn = Turn.enemyTurn;
            UpdateButtonState(false);
            StartCoroutine(EnemyAttack(currentCharacter));
        }
    }

    public void PlayerAttack(int playerIndex)
    {
        if (turn != Turn.playerTurn) return;

        StartCoroutine(PlayerAttackCoroutine(players[playerIndex]));
    }

    IEnumerator PlayerAttackCoroutine(Character player)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log($"{player.name}이(가) 공격을 했습니다.");

        NextTurn();

    }

    IEnumerator EnemyAttack(Character enemy)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log($"{enemy.name}이(가) 공격을 했습니다.");

        NextTurn();
    }
    private void CreateAttackButtons()
    {
        foreach (var btn in attackButtons)
        {
            Destroy(btn.gameObject); // 기존 버튼 삭제
        }
        attackButtons.Clear();

        for (int i = 0; i < players.Count; i++)
        {
            GameObject newButton = Instantiate(attackButtonPrefab, buttonPanel);
            newButton.GetComponentInChildren<Text>().text = players[i].name;
            int index = i; // 람다 캡처 방지
            newButton.GetComponent<Button>().onClick.AddListener(() => PlayerAttack(index));
            attackButtons.Add(newButton.GetComponent<Button>());
        }
    }
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
}
