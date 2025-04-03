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
        if (UIManager.instance != null && characterData != null)
        {
            UIManager.instance.SetBattleCharacterOnData(characterData);
        }
    }

}
