using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MOON : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("广矫累");
  
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().critical.AddMultiples(0.3f);
        }
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("广场");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().critical.AddMultiples(0.3f);
        }
    }
    protected override void SetText()
    {
        TextUi.GetComponentInChildren<TextMeshProUGUI>(true).text = "广涝聪促";
    }
}
