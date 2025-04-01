using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillType
{
    Dosa,
    KumGeok,
    KiSeng,
    Buddhist,
    Hunter,
    Confucianism

}

public enum SkillStatType
{
    Health,
    Attack,
    Defense,
    Critical,
    Evasion,
    Accraucy,
    Speed

}


public enum SkillUsingType
{
    Mana,
    Ki,
    Blood

}

[CreateAssetMenu(fileName ="New SKill", menuName ="Scriptable/Skill")]
public class SKilldata : ScriptableObject
{
    [Header("Skill Basic Info")]
    public float skillDamage;
    public int skillTargetCount;
    public bool isHeal;
    public SkillType skillType;
    public SkillUsingType skillUsingType;
    public float UsingValue;
    public bool isEnemySkill;
    [Header("Buff/Debuff Info")]
    public SkillStatType skillStatType;
    public bool isMulti;
    public bool isBuff;
    public bool isDebuff;
    public float multiValue;
    public int duration;

    [Header("SkillDesInfo")]
    public SkillText skillText;
    public Sprite Icon;
}

[Serializable]
public class SkillText
{
    public string skillNametext;
    public string skillDestext;
    public string skillAllAttactext;

    
}

