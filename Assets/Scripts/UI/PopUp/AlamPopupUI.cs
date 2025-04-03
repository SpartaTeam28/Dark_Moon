using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlamPopupUI : MonoBehaviour
{
    public GameObject parent;
    public Button checkButton;
    public TextMeshProUGUI alarmText;


    public void OnClickCheckButton()
    {
        parent.SetActive(false);
    }

    public void ChangeAlarmText(string text)
    {
        alarmText.text = text;
    }

}
