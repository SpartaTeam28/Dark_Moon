using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSet : MonoBehaviour
{

    private HUD hud;

    public TextMeshProUGUI[] textMeshes;

    private void Awake()
    {
        hud = GetComponentInParent<HUD>();
    }
    private void Start()
    {
        textMeshes = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        Set();
    }

    private void Set()
    {
        if(ClickManager.Instance.sKilldata != null)
        {
            textMeshes[0].text = ClickManager.Instance.sKilldata.skillText.skillNametext;
            textMeshes[1].text = ClickManager.Instance.sKilldata.skillText.skillDestext;
            textMeshes[2].text = ClickManager.Instance.sKilldata.skillText.skillAllAttactext;
        }else
        {
            textMeshes[0].text = string.Empty;
            textMeshes[1].text = string.Empty;
            textMeshes[2].text = string.Empty;

        }
    }







}
