using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUN : Weather
{

    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("�¾����");
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("�¾糡");
    }

}
