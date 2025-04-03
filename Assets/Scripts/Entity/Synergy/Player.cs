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
    Confusianism,
    MoDang
}

public class Player : MonoBehaviour 
{
    public PlayerType playerType;






    private void OnMouseOver()
    {
        if (ClickManager.Instance.isAttacking)
        {
            if (ClickManager.Instance.GetSkilldata().isBuff)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        
    }

    private void OnMouseExit()
    {
        if (ClickManager.Instance.isAttacking)
        {
            if (ClickManager.Instance.GetSkilldata().isBuff)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }





}

