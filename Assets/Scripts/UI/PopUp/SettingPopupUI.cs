using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopupUI : MonoBehaviour
{
    public Button backButton;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public void OnClickCloseButton()
    {
        gameObject.SetActive(false);
    }

}
