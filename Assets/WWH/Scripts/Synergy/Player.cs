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
    private void Awake()
    {
        skillBook = GetComponent<SkillBook>();
    }

    private void OnMouseDown()
    {
        ClickManager.Instance.skillBook = skillBook;
    }


}

