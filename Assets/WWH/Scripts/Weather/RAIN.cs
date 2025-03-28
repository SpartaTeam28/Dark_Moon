using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAIN : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("비시작");
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("비끝");
    }
}
