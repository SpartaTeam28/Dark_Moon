using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSet : MonoBehaviour
{


    public TextMeshProUGUI[] textMeshes;

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
        if(ClickManager.Instance.GetSkilldata() != null)
        {
            textMeshes[0].text = ClickManager.Instance.GetSkilldata().skillText.skillNametext;
            textMeshes[1].text = ClickManager.Instance.GetSkilldata().skillText.skillDestext;
            textMeshes[2].text = ClickManager.Instance.GetSkilldata().skillText.skillAllAttactext;
        }else
        {
            textMeshes[0].text = string.Empty;
            textMeshes[1].text = string.Empty;
            textMeshes[2].text = string.Empty;

        }
    }







}
