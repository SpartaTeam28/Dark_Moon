using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{

    [SerializeField][Range(0f,50f)] float value;
    public virtual void WeatherEnter()
    {

    }
    public virtual void WeatherLeave() 
    {

    }

}
