using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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




    private void OnMouseOver()
    {
        if (ClickManager.Instance.isAttacking)
        {
            if (ClickManager.Instance.skillData.isBuff)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        
    }

    private void OnMouseExit()
    {
        if (ClickManager.Instance.isAttacking)
        {
            if (ClickManager.Instance.skillData.isBuff)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        skillBook = GetComponent<SkillBook>();
        characterStat = GetComponent<CharacterStat>();
    }




}

