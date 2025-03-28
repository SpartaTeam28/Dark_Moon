using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWeather : MonoBehaviour
{

    private List<GameObject> weatherList = new List<GameObject>();

    private void Awake()
    {
        weatherList.Add((GameObject)Resources.Load("WWHPrefabs/SUN"));
        weatherList.Add((GameObject)Resources.Load("WWHPrefabs/MOON"));
        weatherList.Add((GameObject)Resources.Load("WWHPrefabs/RAIN"));
        weatherList.Add((GameObject)Resources.Load("WWHPrefabs/SNOW"));
        weatherList.Add((GameObject)Resources.Load("WWHPrefabs/FOG"));
    }


    public void WeatherIndexStart(int index)
    {
        GameObject weather = Instantiate(weatherList[index], transform);
        weather.SetActive(true);
        weather.GetComponent<Weather>().WeatherEnter();
    }
    public void WeatherRandomStart()
    {
        GameObject weather = Instantiate(weatherList[Random.Range(0, weatherList.Count)], transform);
        weather.SetActive(true);
        weather.GetComponent<Weather>().WeatherEnter();
    }

    public void WeatherChange()
    {
        WeatherEnd();
        WeatherRandomStart();
    }

    public void WeatherEnd()
    {
        transform.GetComponentInChildren<Weather>().WeatherLeave();
        Destroy(transform.GetChild(0).gameObject);
    }






}
