using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public enum Turn
{
    start,
    enemyTurn,
    playerTurn,
    win,
    lose
}
public class Battle : MonoBehaviour
{
    //
    public Turn turn; // �� ����
    public bool isAlive; // ����ִ°�
    public Transform buttonPanel; // ��ư���� ��ġ�� UI �г�
    public GameObject attackButtonPrefab; // ��ư ������ (Inspector���� ����
    public bool isLock; // ���

    List<Character> players;
    List<Character> enemies;  // �� ����Ʈ
    List<Character> turnOrder; // �� ���� ����Ʈ
    int currentTurnIndex = 0; // ���� �� ���� ���� ĳ���� �ε���

    private List<Button> attackButtons = new List<Button>(); // ��ư ����Ʈ


    private void Awake()
    {
        turn = Turn.start; // ���� ����
        BattleStart();
    }

    public void BattleStart()
    {
        // ���ǵ� ���ؼ� �� ���ϱ�
        SpeedCheck();
        CreateAttackButtons();
        NextTurn();
    }

    public void SpeedCheck()
    {
        // �÷��̾�� �� ����Ʈ�� �ϳ��� ����Ʈ�� ��ģ �� �ӵ� �� ����
        turnOrder = new List<Character>();
        turnOrder.AddRange(players);
        turnOrder.AddRange(enemies);
        turnOrder = turnOrder.OrderByDescending(c => c.speed).ToList(); // �ӵ� ���� �������� ����
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
