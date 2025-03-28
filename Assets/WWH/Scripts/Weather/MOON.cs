using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOON : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("π„Ω√¿€");
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("π„≥°");
    }
}
