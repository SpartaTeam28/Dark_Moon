using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DesText : MonoBehaviour
{

    public TextMeshProUGUI[]  textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponentsInChildren<TextMeshProUGUI>();
    }


    //private void Update()
    //{
        
    //}

    //public void Reset()
    //{
    //    if (ClickManager.Instance.skillBook != null)
    //    {

    //        //textMeshProUGUI[0].text = ClickManager.Instance.skillBook.SilhumSkill[0].skillText.skillNametext;

    //    }
    //}


}
