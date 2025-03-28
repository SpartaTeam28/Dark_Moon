using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public enum UIState
{
    Title, //0
    CharacterSelect, // 1
    Lobby // 2


}



public class UIManager : MonoBehaviour
{
    public UIState currentState = UIState.Title;
    public UIState prevState = UIState.Title;

    TitleUI titleUI = null;
    CharacterSelectUI characterSelectUI = null;
    LobbyUI lobbyUI = null;


    public string playerName { get; private set; }

    private static UIManager _instance;



    public static UIManager instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (_instance != this)
                Destroy(this.gameObject);
        }

        titleUI = GetComponentInChildren<TitleUI>(true);
        titleUI?.Init(this);
        characterSelectUI = GetComponentInChildren<CharacterSelectUI>(true);
        characterSelectUI.Init(this);
        lobbyUI = GetComponentInChildren<LobbyUI>(true);
        lobbyUI.Init(this);
    }

    private void Start()
    {
        ChangeState(currentState);
    }

    public void ChangeState(UIState state) //UI������Ʈ�� on off ���ִ� ���
    {
        currentState = state; //�Ʒ����� �ش��ϴ� UI������Ʈ�� ã�� on off ����
        titleUI?.SetActive(currentState);
        characterSelectUI?.SetActive(currentState);
        lobbyUI?.SetActive(currentState);
        

        //lobbyUI?.SetActive(currentState);
        //gameUI?.SetActive(currentState);
        //pauseUI?.SetActive(currentState);
        //gameOverUI?.SetActive(currentState);
        //settingUI?.SetActive(currentState);
        //achievementUI?.SetActive(currentState);
        //customizeUI?.SetActive(currentState);
    }

    public void OnClickStart() //�����ϱ⸦ �������
    {
        ChangeState(UIState.CharacterSelect);
    }

    public void OnClickExit() //���� ���Ḧ ���� ���
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }

    public void OnClickLobby()
    {
        ChangeState(UIState.Lobby);
    }

    public void OnClickBack()
    {
        ChangeState(prevState);
    }


    public void SetPlayerName()
    {
        playerName = characterSelectUI.nickNamePopup.nickNameText.text.ToString();
    }


}
