using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClick : MonoBehaviour
{
    private Character characterData;

    private void Start()
    {
        characterData = GetComponent<Character>(); // ĳ���� ������ ��������
    }

    private void OnMouseDown()
    {
        if (CommonBattleUI.instance != null && characterData != null)
        {
            CommonBattleUI.instance.SetCharacterData(characterData);
            CommonBattleUI.instance.ShowUI(true); // UI ǥ��
        }
    }

}
