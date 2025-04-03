using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MOON : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("¹ã½ÃÀÛ");
  
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().critical.AddMultiples(0.3f);
        }
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("¹ã³¡");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().critical.AddMultiples(-0.3f);
        }
    }
    protected override void SetText()
    {
        TextUi.GetComponentInChildren<TextMeshProUGUI>(true).text = "¹ã\n¸ðµç ¾Æ±ºÀÇ Å©¸®Æ¼ÄÃ È®·ü 30% Áõ°¡";
    }
}
