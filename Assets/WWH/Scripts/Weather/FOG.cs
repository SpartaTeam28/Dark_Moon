using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOG : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();

        foreach(Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().accuracy.AddMultiples(-0.3f);
        }
 
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().accuracy.AddMultiples(0.3f);
        }
    }
}
