using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weather : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

  
    protected GameObject TextUi;

    private void Start()
    {
        TextUi = transform.GetChild(0).gameObject;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        TextUi.SetActive(true);
        SetText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       TextUi.SetActive(false);
    }


    public virtual void WeatherEnter()
    {

    }
    public virtual void WeatherLeave() 
    {

    }

    protected virtual void SetText()
    {
     

    }
 
   
}
