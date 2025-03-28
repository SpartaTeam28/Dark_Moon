using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOG : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("안개시작");
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("안개끝");
    }
}
