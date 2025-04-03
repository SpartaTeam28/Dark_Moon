using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherChanger : MonoBehaviour
{
    public StageWeather stageWeather;

    private void Start()
    {
        stageWeather = GetComponent<StageWeather>();
        //stageWeather.WeatherRandomStart();
    }


    




}
