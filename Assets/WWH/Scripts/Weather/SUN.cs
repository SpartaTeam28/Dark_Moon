using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SUN : Weather
{

    public override void WeatherEnter()
    {
        base.WeatherEnter();

        Debug.Log("�¾����");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().defence.AddMultiples(0.3f);
        }
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("�¾糡");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().defence.AddMultiples(0.3f);
        }
    }

    protected override void SetText()
    {
        TextUi.GetComponentInChildren<TextMeshProUGUI>(true).text = "�¾��Դϴ�";
    }
}
