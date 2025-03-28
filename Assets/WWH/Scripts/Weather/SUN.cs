using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUN : Weather
{

    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("태양시작");
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("태양끝");
    }

}
