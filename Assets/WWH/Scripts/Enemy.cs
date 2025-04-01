using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    private void OnMouseOver()
    {
        if(ClickManager.Instance.isAttacking)
        {
            if(!ClickManager.Instance.skillData.isBuff && !ClickManager.Instance.skillData.isHeal)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
        if (ClickManager.Instance.isAttacking)
        {
            if (!ClickManager.Instance.skillData.isBuff && !ClickManager.Instance.skillData.isHeal)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
