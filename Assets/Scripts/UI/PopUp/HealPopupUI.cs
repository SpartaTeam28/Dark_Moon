using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealPopupUI : MonoBehaviour
{
    public Button checkButton;
    public Button exitButton;

    public int healGold;


    private void Awake()
    {
        //checkButton.onClick.AddListener(OnClickHeal);
        //exitButton.onClick.AddListener(OnClickCloseButton);
        healGold = 500;
    }


    public void OnClickHeal()
    {
        //Heal
        UIManager.instance.SpenGold(healGold);

        for(int i = 0; i < GameManager.instance.friendlyCharacterList.Count; i++)
        {
            GameManager.instance.friendlyCharacterList[i].stat.health.AddHealth(100); 
        }
    }

    public void OnClickCloseButton()
    {
        this.gameObject.SetActive(false);
    }

}
