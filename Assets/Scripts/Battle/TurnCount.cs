using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnCount : MonoBehaviour
{
    private TextMeshProUGUI proUGUI;
    private void Awake()
    {
        proUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }


    private void Update()
    {
        if (proUGUI != null) 
        {
            proUGUI.text = Battle_Silhum.Instance.TurnCount.ToString();
        }
    }
}
