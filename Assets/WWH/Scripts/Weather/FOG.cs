using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOG : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("�Ȱ�����");
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("�Ȱ���");
    }
}
