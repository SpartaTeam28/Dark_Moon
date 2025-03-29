using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SUN : Weather
{

    public override void WeatherEnter()
    {
        base.WeatherEnter();

        Debug.Log("태양시작");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().defence.AddMultiples(0.3f);
        }
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("태양끝");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().defence.AddMultiples(0.3f);
        }
    }

    protected override void SetText()
    {
        TextUi.GetComponentInChildren<TextMeshProUGUI>(true).text = "태양입니다";
    }
}
