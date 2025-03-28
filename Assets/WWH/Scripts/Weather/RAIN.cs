using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAIN : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("비시작");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().evasion.AddMultiples(0.3f);
        }
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("비끝");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().evasion.AddMultiples(-0.3f);
        }
    }
}
