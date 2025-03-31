using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SNOW : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("传矫累");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().speed.AddStat(-3);
        }
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("传场");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().speed.AddStat(+3);
        }
    }

    protected override void SetText()
    {
        TextUi.GetComponentInChildren<TextMeshProUGUI>(true).text = "气汲\n酒焙狼 加档 3 皑家";
    }
}
