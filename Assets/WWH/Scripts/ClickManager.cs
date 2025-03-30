using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

    private static ClickManager instance;
    public static ClickManager Instance { get { return instance; } set { instance = value; } }

    public CharacterStat characterStat;
    public SkillBook skillBook;
    public SKilldata skillData;
    public bool IsBuff;

    public bool isAttacking;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {

        if (isAttacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 WorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray ray = new Ray(WorldPoint, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);


                if (hit.transform != null)
                {
                    if (hit.transform.CompareTag("Enemy") && skillData.skillTargetCount == 1)
                    {
                        OnePersonAttack(hit.transform.GetComponent<CharacterStat>());
                    }
                    if (hit.transform.CompareTag("Enemy") && skillData.skillTargetCount > 1)
                    {
                        AllPersomAttack(hit.transform.GetComponent<CharacterStat>());
                    }
                    if (hit.transform.CompareTag("Enemy") && skillData.isDebuff)
                    {
                        Debuff(hit.transform.GetComponent<CharacterStat>());
                    }
                    if (hit.transform.CompareTag("Friends") && skillData.isBuff)
                    {
                 
                        Buff(hit.transform.GetComponent<CharacterStat>());
  
                    }
                    AttackEnd();
                }
            }
        }
    }



    public void SetCharecterStat(CharacterStat character)
    {
        characterStat = character;
    }
    public void SetSkillBook(SkillBook skill)
    {
        if (!IsBuff)
        {
            skillBook = skill;
            if (skillData != null)
            {
                skillData = null;
            }
            isAttacking = false;
        }


    }

    public void SetSkill(int index)
    {
        skillData = skillBook.SilhumSkill[index];
        isAttacking = true;
        if(skillData.isBuff)
        {
            IsBuff = true;
        }
        else
        {
            IsBuff = false;
        }
    }



    public void OnePersonAttack(CharacterStat stat)
    {
        Debug.Log("Attack"); 


    }

    public void AllPersomAttack(CharacterStat stat)
    {
        Debug.Log("Multi Attack");
   
    }
    public void Debuff(CharacterStat stat)
    {
        Debug.Log("Debuff");

    }

    public void Buff(CharacterStat stat)
    {
        //Heal Is Here
        Debug.Log("Buff");

        IsBuff = false;
   

    }

    //public IEnumerator Buff()
    //{
    //    yield return new WaitUntil() 특정 턴 수
    //}

    //public IEnumerator DeBuff()
    //{
    //    yield return new WaitUntil() 특정 턴 수
    //}


    public void AttackEnd()
    {
        characterStat = null;
        skillBook = null;
        skillData = null;
        isAttacking =false;
    }
    public bool IsEnoughMana(CharacterStat character)
    {
        return true;
    }





}
