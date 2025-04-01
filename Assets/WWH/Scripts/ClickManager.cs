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
    public bool IsSilhum = false;

    public delegate void Next();
    public Next next;
    private void Awake()
    {
        if (instance == null)
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

            TargetSet();
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 WorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray ray = new Ray(WorldPoint, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (IsEnoughMana(characterStat))
                {
                    if (hit.transform != null)
                    {
                        if(skillData.isDebuff )
                        {
                            if(hit.transform.CompareTag("Friends"))
                            {
                                return;
                            }
                        }
                        else if (skillData.isBuff || skillData.isHeal )
                        {
                            if (hit.transform.CompareTag("Enemy"))
                            {
                                return;
                            }
                        }
                        if (skillData.skillTargetCount == 1)
                        {
                            OnePersonAttack(hit.transform.GetComponent<Character>(), hit);
                        }
                        else if(skillData.skillTargetCount>1)
                        {
                            AllPersonAttack();
                        }
                        AttackEnd();

                    }
                    else
                    {
                        AttackEnd();
                    }
                }
                else
                {
                    AttackEnd();
                    Debug.Log("Not Enough Mana");
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
            TargetDown();
        }


    }

    public void SetSkill(int index)
    {
        skillData = skillBook.SilhumSkill[index];
        isAttacking = true;
        TargetDown();
        if (skillData.isBuff || skillData.isHeal)
        {
            IsBuff = true;
        }
        else
        {
            IsBuff = false;
        }
    }



    public void OnePersonAttack(Character stat, RaycastHit2D hit)
    {
        SKilldata skillData = this.skillData;
        if(skillData.isDebuff && hit.transform.CompareTag("Enemy")) 
        {
            Debuff(hit.transform.GetComponent<CharacterStat>());
            next.Invoke();
        }
        else if(skillData.isBuff)
        {
            IsBuff = false;
            Buff(hit.transform.GetComponent<CharacterStat>());
            next.Invoke();
        }
        else if(skillData.isHeal)
        {
            IsBuff = false;
            Heal(hit.transform.GetComponent<CharacterStat>());
            next.Invoke();
        }
        else
        {
            //Attack
            stat.TakeDamaged(skillData.skillDamage);
            ManaSub(skillData);
            TargetDown();
            next.Invoke();
        }

    }

    public void AllPersonAttack()
    {

        SKilldata sKilldata = skillData;
        if (skillData.isBuff)
        {
            IsBuff = false;
            foreach (Character character in GameManager.instance.friendlyCharacterList)
            {
           
                Buff(character.stat);
                
            }
            next.Invoke();
        }
        else if (skillData.isDebuff)
        {
            foreach (Character character in GameManager.instance.EnemyCharacterList)
            {
                Debuff(character.stat);
            }
            next.Invoke();
        }
        else if (skillData.isHeal)
        {
            IsBuff = false;
            foreach (Character character in GameManager.instance.friendlyCharacterList)
            {
                Heal(character.stat);
            }
            next.Invoke();
        }
        else
        {
            foreach (Character character in GameManager.instance.EnemyCharacterList)
            {
             
                character.TakeDamaged(sKilldata.skillDamage);
                ManaSub(sKilldata);
                TargetDown();
        
            }
            next.Invoke();
        }

   

    }

    public void Heal(CharacterStat character, SKilldata sKillData = null)
    {
        if(sKillData != null)
        {
            skillData = sKillData;
        }
        SKilldata sKilldata = skillData;
        character.health.AddHealth(skillData.skillDamage);
        ManaSub(sKilldata);
        //TargetDown();
     
    }
    public void Debuff(CharacterStat stat, SKilldata sKilldata = null)
    {
        if (sKilldata != null)
        {
            skillData = sKilldata;
        }
        switch (skillData.skillStatType)
        {
            case SkillStatType.Attack:
                StartCoroutine(DeBuffStart(stat.attack));
                break;
            case SkillStatType.Critical:
                StartCoroutine(DeBuffStart(stat.critical));
                break;
            case SkillStatType.Defense:
                StartCoroutine(DeBuffStart(stat.defence));
                break;
            case SkillStatType.Evasion:
                StartCoroutine(DeBuffStart(stat.evasion));
                break;
            case SkillStatType.Speed:
                StartCoroutine(DeBuffStart(stat.speed));
                break;
        }
        //TargetDown();
    }

    public void Buff(CharacterStat stat , SKilldata sKilldata = null)
    {    
     
        if(sKilldata != null)
        {
            skillData = sKilldata;
        }

        switch (skillData.skillStatType)
        {
            case SkillStatType.Attack:
                StartCoroutine(BuffStart(stat.attack));
                break;
            case SkillStatType.Critical:
                StartCoroutine(BuffStart(stat.critical));
                break;
            case SkillStatType.Defense:
                StartCoroutine(BuffStart(stat.defence));
                break;
            case SkillStatType.Evasion:
                StartCoroutine(BuffStart(stat.evasion));
                break;
            case SkillStatType.Health:
                StartCoroutine(BuffStart(stat.health));
                break;
            case SkillStatType.Speed:
                StartCoroutine(BuffStart(stat.speed));
                break;
        }
        //TargetDown();
       
    }

    public IEnumerator BuffStart(BaseStat stat)
    {
        SKilldata sKilldata = skillData;
        ManaSub(sKilldata);
        if (skillData.isMulti)
        {
            stat.AddMultiples(skillData.multiValue);
        }
        else
        {
            stat.AddStat(skillData.skillDamage);
        }
        
        yield return new WaitUntil(() => IsSilhum) ;
        if (sKilldata.isMulti)
        {
            stat.AddMultiples(-sKilldata.multiValue);
        }
        else
        {
            stat.AddStat(-sKilldata.skillDamage);
        }

    }

    public IEnumerator DeBuffStart(BaseStat stat)
    {
        SKilldata sKilldata = skillData;
        ManaSub(sKilldata);
        if (sKilldata.isMulti)
        {
            stat.AddMultiples(-skillData.multiValue);
        }
        else
        {
            stat.AddStat(-skillData.skillDamage);
        }
        yield return new WaitUntil(() => IsSilhum);
        if (sKilldata.isMulti)
        {
            stat.AddMultiples(sKilldata.multiValue);
        }
        else
        {
            stat.AddStat(sKilldata.skillDamage);
        }
    }

    public void AttackEnd()
    {
        TargetDown();
        skillData = null;
        isAttacking =false;
    }
    public bool IsEnoughMana(CharacterStat character)
    {
        if(character.mana.curMana >= skillData.UsingValue)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    public void TargetSet()
    {
        if (skillData.skillTargetCount > 1)
        {
            if (skillData.isHeal || skillData.isBuff)
            {
                foreach (Character character in GameManager.instance.friendlyCharacterList)
                {
                    character.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Character character in GameManager.instance.EnemyCharacterList)
                {
                    character.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }


    public void TargetDown()
    {
        foreach (Character character in GameManager.instance.friendlyCharacterList)
        {
            character.transform.GetChild(0).gameObject.SetActive(false);
        }
        foreach (Character character in GameManager.instance.EnemyCharacterList)
        {
            character.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void ManaSub(SKilldata sKilldata)
    { 
        if(skillData ==  null || skillData.isEnemySkill)
        {
            return;
        }
        characterStat.mana.curMana -= sKilldata.UsingValue;
    }

    //public void CoolTimeSet()
    //{
        
    //}







}
