using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Battle_Silhum : MonoBehaviour
{
    //
    public Turn turn; // �� ����
    public bool isAlive; // ����ִ°�
    public Transform buttonPanel; // ��ư���� ��ġ�� UI �г�
    public GameObject attackButtonPrefab; // ��ư ������ (Inspector���� ����
    public bool isLock; // ���

    public List<Character> players;
    public List<Character> enemies;  // �� ����Ʈ
    public List<Character> turnOrder; // �� ���� ����Ʈ
    int currentTurnIndex = 0; // ���� �� ���� ���� ĳ���� �ε���

    private List<Button> attackButtons = new List<Button>(); // ��ư ����Ʈ

    [Header("���������� �� ����Ʈ")]
    [SerializeField] public List<Character> UlsanEnemies;
    [SerializeField] private List<Character> BusanEnemies;
    [SerializeField] private List<Character> HanbatEnemies;
    [SerializeField] private List<Character> DeaguEnemies;

    private void Awake()
    {
        //players = new List<Character>(GameManager.Instance.friendlyCharacterList);
        LoadEnemies();
        turn = Turn.start; // ���� ����

    }

    private void Start()
    {
        BattleStart();
    }

    public void BattleStart()
    {
        // ���ǵ� ���ؼ� �� ���ϱ�
        SpeedCheck();
        //CreateAttackButtons();
        NextTurn();
    }
    public void LoadEnemies()
    {
        // ���������� �´� �� ����Ʈ �ε�
        Debug.Log($"UlsanEnemies Count: {DeaguEnemies.Count}"); // �� ���� ���� �ִ��� Ȯ��

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
                Debug.LogError($"�� ����Ʈ�� �����ϴ�! ���� ��: {currentScene}");
                break;
        }
        Debug.Log($"Enemies Count after LoadEnemies(): {enemies.Count}"); // enemies�� ���������� �����ƴ��� Ȯ��
    }

    public void SpeedCheck()
    {
        // �÷��̾�� �� ����Ʈ�� �ϳ��� ����Ʈ�� ��ģ �� �ӵ� �� ����
        turnOrder = new List<Character>();
        turnOrder.AddRange(players);
        turnOrder.AddRange(enemies);
        // ����? ���
        turnOrder = turnOrder.OrderByDescending(c => c.stat.speed.value).ToList();
        currentTurnIndex = 0; // ù ��° ĳ���ͺ��� ����
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
            Debug.Log($"{currentCharacter.name}�� ���Դϴ�. ���� ��ư�� ���� �����ϼ���.");
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
        Debug.Log($"{player.name}��(��) ������ �߽��ϴ�.");

        NextTurn();

    }

    IEnumerator EnemyAttack(Character enemy)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log($"{enemy.name}��(��) ������ �߽��ϴ�.");

        NextTurn();
    }
    private void CreateAttackButtons()
    {
        foreach (var btn in attackButtons)
        {
            Destroy(btn.gameObject); // ���� ��ư ����
        }
        attackButtons.Clear();

        for (int i = 0; i < players.Count; i++)
        {
            GameObject newButton = Instantiate(attackButtonPrefab, buttonPanel);
            newButton.GetComponentInChildren<Text>().text = players[i].name;
            int index = i; // ���� ĸó ����
            newButton.GetComponent<Button>().onClick.AddListener(() => PlayerAttack(index));
            attackButtons.Add(newButton.GetComponent<Button>());
        }
    }
    public void EndBattle()
    {
        Debug.Log("���� ��");
    }

    private void UpdateButtonState(bool active)
    {
        foreach (var btn in attackButtons)
        {
            btn.interactable = active;
        }
    }
}
