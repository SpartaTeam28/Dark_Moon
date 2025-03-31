using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommonBattleUI : MonoBehaviour
{
    public static CommonBattleUI instance;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenceText;
    public TextMeshProUGUI criticalText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI evasionText;
    public TextMeshProUGUI speedText;

    public GameObject statPanel;

    public List<Character> battleCharacters;
    //public List<Character> enemyCharacters;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //battleCharacters = GameManager.instance.friendlyCharacterList;
        //enemyCharacters = GameManager.instance.EnemyCharacterList;
        ShowUI(false); // 게임 시작 시 UI 숨기기
    }


    public void SetCharacterData(Character anonymouscharacter)
    {
        healthText.text = anonymouscharacter.stat.health.GetValueToString();
        manaText.text = anonymouscharacter.stat.mana.GetValueToString();
        attackText.text = anonymouscharacter.stat.attack.GetValueToString();
        defenceText.text = anonymouscharacter.stat.defence.GetValueToString();
        criticalText.text = anonymouscharacter.stat.critical.GetValueToString();
        accuracyText.text = anonymouscharacter.stat.accuracy.GetValueToString();
        evasionText.text = anonymouscharacter.stat.evasion.GetValueToString();
        speedText.text = anonymouscharacter.stat.speed.GetValueToString();
    }

    // UI 표시/숨김 기능 추가
    public void ShowUI(bool isVisible)
    {
        if (statPanel != null)
        {
            statPanel.SetActive(isVisible);
        }
    }

}
