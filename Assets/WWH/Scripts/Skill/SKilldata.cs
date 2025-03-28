using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillType
{
    Dosa,
    GumGeok,
    GiSeng,
    Buddhist,
    Hunter,
    Confucianism

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
    [Header("Buff/Debuff Info")]
    public bool isBuff;
    public bool isDebuff;
    public int duration;





}
