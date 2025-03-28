using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NickNamePopupUI : MonoBehaviour
{
    public Button checkButton;
    public Button closeButton;
    public TextMeshProUGUI nickNameText;


    public void Awake()
    {
        checkButton.onClick.AddListener(OnClickLobby);
        closeButton.onClick.AddListener(OnClickClose);    
    }

    public void OnClickLobby()
    {
        UIManager.instance.SetPlayerName();
        UIManager.instance.OnClickLobby();
    }

    public void OnClickClose()
    {
        this.gameObject.SetActive(false);
    }


}
