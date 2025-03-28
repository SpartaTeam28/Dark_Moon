using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNOW : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("´«½ÃÀÛ");
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("´«³¡");
    }
}
