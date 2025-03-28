using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNOW : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("´«½ÃÀÛ");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().attack.AddMultiples(0.3f);
        }
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("´«³¡");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().attack.AddMultiples(-0.3f);
        }
    }
}
