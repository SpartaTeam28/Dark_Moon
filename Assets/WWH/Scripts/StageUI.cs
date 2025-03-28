using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{

    public Button[] buttons;
    public Image[] images;
    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        images = GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        Set();
    }

    public void Set()
    {
        if(ClickManager.Instance.skillBook != null)
        {
            for(int i = 0; i < images.Length; i++) 
            {
                images[i].sprite = ClickManager.Instance.skillBook.SilhumSkill[i].Icon;
            }
          
        }
    }


}
