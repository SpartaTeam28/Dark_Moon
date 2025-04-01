using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJob", menuName = "Jobs/Job")]
public class JobData : ScriptableObject
{
    public string jobName;
    public bool isMale;
    public Sprite sprite;
    public Sprite icon;
    public AnimatorOverrideController animatorController;

    [Header("Inintialize value")]
    public float attck;//공격력 초기값
    public float defence;//방어력 초기값
    public float maxHealth;//체력 초기값
    public float curHealth;
    public float maxMana;
    public float curMana;
    public float critical;//치명타 초기값
    public float accurracy;
    public float evasion;
    public float speed;

    [Header("Levelup value")]
    public float levelAttck;
    public float levelDefence;
    public float levelMaxHealth;
    public float levelMaxMana;
    public float levelCritical;
    public float levelAccurracy;
    public float leveltEvasion;
    public float levelSpeed;

    public JobData Clone()
    {
        JobData copy = CreateInstance<JobData>();

        copy.jobName = this.jobName;
        copy.isMale = this.isMale;
        copy.sprite = this.sprite;
        copy.icon = this.icon;
        copy.animatorController = this.animatorController;

        copy.attck = this.attck;
        copy.defence = this.defence;
        copy.maxHealth = this.maxHealth;
        copy.curHealth = this.curHealth;
        copy.maxMana = this.maxMana;
        copy.curMana = this.curMana;
        copy.critical = this.critical;
        copy.accurracy = this.accurracy;
        copy.evasion = this.evasion;
        copy.speed = this.speed;

        copy.levelAttck = this.levelAttck;
        copy.levelDefence = this.levelDefence;
        copy.levelMaxHealth = this.levelMaxHealth;
        copy.levelMaxMana = this.levelMaxMana;
        copy.levelCritical = this.levelCritical;
        copy.levelAccurracy = this.levelAccurracy;
        copy.leveltEvasion = this.leveltEvasion;
        copy.levelSpeed = this.levelSpeed;

        return copy;
    }
}
