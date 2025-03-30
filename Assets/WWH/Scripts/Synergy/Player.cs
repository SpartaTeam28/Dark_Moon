using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum PlayerType
{
    Dosa,
    KumGeok,
    KiSeng,
    Buddhist,
    Hunter,
    Confusianism
}

public class Player : MonoBehaviour
{
    public PlayerType playerType;
    public SkillBook skillBook;
    public CharacterStat characterStat;
    private void Awake()
    {
        skillBook = GetComponent<SkillBook>();
        characterStat = GetComponent<CharacterStat>();
    }

    private void OnMouseDown()
    {
        ClickManager.Instance.SetSkillBook(skillBook);
        ClickManager.Instance.SetCharecterStat(characterStat);
    }


}

