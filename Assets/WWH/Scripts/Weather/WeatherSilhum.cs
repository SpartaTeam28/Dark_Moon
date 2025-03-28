using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSilhum : MonoBehaviour
{
    public StageWeather stageWeather;
    float ChangeTime = 7f;
    float time;

    private void Start()
    {
        stageWeather.WeatherRandomStart();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time > ChangeTime)
        {
            time -= ChangeTime;
            stageWeather.WeatherChange();
        }
    }

    public void OnDisable()
    {
        stageWeather.WeatherEnd();
    }


}
