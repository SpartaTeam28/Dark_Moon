using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlamPopupUI : MonoBehaviour
{
    public GameObject parent;
    public Button checkButton;


    public void OnClickCheckButton()
    {
        parent.SetActive(false);
    }

}
