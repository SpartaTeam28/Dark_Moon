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
    public Turn turn; // �� ����
    public bool isAlive; // ����ִ°�
    public Transform buttonPanel; // ��ư���� ��ġ�� UI �г�
    public GameObject attackButtonPrefab; // ��ư ������ (Inspector���� ����
    public bool isLock; // ���
    public int TurnCount = 0;

    
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
    [SerializeField] private List<Character> BattleScene;
    private int playerDeathCount =  0;
    private int enemyDeathCount = 0;


    public Transform speedTextPanel; // UI���� Speed ���� ǥ���� Panel
    public GameObject speedTextPrefab; // Speed ���� ǥ���� Text ������
    private StageWeather StageWeather;
    private Dictionary<Character, TextMeshProUGUI> speedTexts = new Dictionary<Character, TextMeshProUGUI>();
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
        ClickManager.Instance.next = NextTurn;
        playerDeathCount = GameManager.instance.friendlyCharacterList.Count;
        enemyDeathCount = (int)(GameManager.instance.EnemyCharacterList?.Count);
        StageWeather = FindAnyObjectByType<StageWeather>();
        StageWeather.WeatherRandomStart();

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
            case "Battle_Scene":
                enemies = new List<Character>(BattleScene);
                Debug.Log($"HanbatEnemies Count: {BattleScene.Count}");
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

        Debug.Log("=== ĳ���� Speed ���� ===");
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
                
        Debug.Log(currentTurnIndex.ToString() + "���� �ε���");

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
        Debug.Log($"{enemy.name}��(��) ������ �߽��ϴ�.");
        yield return new WaitForSeconds(2f);
        ClickManager.Instance.TargetDown();
        NextTurn();
    }
    //private void CreateAttackButtons()
    //{
    //    foreach (var btn in attackButtons)
    //    {
    //        Destroy(btn.gameObject); // ���� ��ư ����
    //    }
    //    attackButtons.Clear();

    //    for (int i = 0; i < players.Count; i++)
    //    {
    //        GameObject newButton = Instantiate(attackButtonPrefab, buttonPanel);
    //        newButton.GetComponentInChildren<Text>().text = players[i].name;
    //        int index = i; // ���� ĸó ����
    //        newButton.GetComponent<Button>().onClick.AddListener(() => PlayerAttack(index));
    //        attackButtons.Add(newButton.GetComponent<Button>());
    //    }
    //}
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
            return;
        }
        if (enemyDeathCount == 0)
        {
            isAlive = false;
            turn = Turn.win;
            return;
        }
      
    }


    private void DisplaySpeedTexts()
    {
        int i = 1;
        foreach (var pair in speedTexts)
        {
            Destroy(pair.Value.gameObject); // ���� UI ����
        }
        speedTexts.Clear();

        foreach (var character in turnOrder)
        {

            GameObject newText = Instantiate(speedTextPrefab, speedTextPanel);
            TextMeshProUGUI textComponent = newText.GetComponent<TextMeshProUGUI>();
            //textComponent.text = $" Speed: {character.stat.speed.value}";
            textComponent.text = $" Speed: {i}";
            Vector3 screenPos = Camera.main.WorldToScreenPoint(character.transform.position);
            newText.transform.position = screenPos + new Vector3(0, 100f, 0); // Y�� ���� (�Ʒ��� ������)

            speedTexts[character] = textComponent;
            i++;
        }
    }
    public void EndGame()
    {
        
    }

    public void WeatherChange()
    {
        if(TurnCount % 5 == 0)
        {
            StageWeather.WeatherChange();
        }
    }
}
