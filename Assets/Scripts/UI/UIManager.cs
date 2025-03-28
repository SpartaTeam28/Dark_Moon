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

    public void ChangeState(UIState state) //UI오브젝트를 on off 해주는 기능
    {
        currentState = state; //아래에서 해당하는 UI오브젝트를 찾아 on off 해줌
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

    public void OnClickStart() //시작하기를 누른경우
    {
        ChangeState(UIState.CharacterSelect);
    }

    public void OnClickExit() //게임 종료를 누른 경우
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
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
