using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    public Button jumagButton;
    public Button healStationButton;

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
}
