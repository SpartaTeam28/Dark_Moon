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
    public bool isCoolTimeSkill;
    public int skillCoolTime;
    public SkillType skillType;
    public SkillUsingType skillUsingType;
    public float UsingValue;
    [Header("Buff/Debuff Info")]
    public bool isBuff;
    public bool isDebuff;
    public int duration;





}
