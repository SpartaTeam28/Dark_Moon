using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlamPopupUI : MonoBehaviour
{
    public GameObject parent;
    public Button checkButton;

    private void Awake()
    {
        checkButton.onClick.AddListener(OnClickCheckButton);
    }

    public void OnClickCheckButton()
    {
        parent.SetActive(false);
    }

}
