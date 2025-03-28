using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOON : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("π„Ω√¿€");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().critical.AddMultiples(0.3f);
        }
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("π„≥°");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().critical.AddMultiples(0.3f);
        }
    }
}
