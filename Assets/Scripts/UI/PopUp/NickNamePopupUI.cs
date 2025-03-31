using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NickNamePopupUI : MonoBehaviour
{
    public Button checkButton;
    public Button closeButton;
    public TMP_InputField nickNameText;

    private TextMeshProUGUI placeholderText;


    public void Awake()
    {
        //checkButton.onClick.AddListener(OnClickLobby);
        //closeButton.onClick.AddListener(OnClickClose);
        
        placeholderText = nickNameText.placeholder as TextMeshProUGUI;
    }

    public void OnClickLobby()
    {
        if(!IsOnlyLetters(nickNameText.text))
        {
            nickNameText.text = "";
            placeholderText.text = "�ѱ��� �Է��ϼ���.";
            return;
        }

        UIManager.instance.SetPlayer(nickNameText.text);
        UIManager.instance.OnClickLobby();
    }

    public void OnClickClose()
    {
        this.gameObject.SetActive(false);
    }

    bool IsOnlyLetters(string text)
    {
        return Regex.IsMatch(text, @"^[��-�R]+$");
    }



}
