using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MOON : Weather
{
    public override void WeatherEnter()
    {
        base.WeatherEnter();
        Debug.Log("�����");
  
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().critical.AddMultiples(0.3f);
        }
    }
    public override void WeatherLeave()
    {
        base.WeatherLeave();
        Debug.Log("�㳡");
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.GetComponent<CharacterStat>().critical.AddMultiples(-0.3f);
        }
    }
    protected override void SetText()
    {
        TextUi.GetComponentInChildren<TextMeshProUGUI>(true).text = "��\n��� �Ʊ��� ũ��Ƽ�� Ȯ�� 30% ����";
    }
}
