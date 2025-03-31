using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJob", menuName = "Jobs/Job")]
public class JobData : ScriptableObject
{
    public string jobName;
    public Sprite sprite;
    public Sprite icon;
    public AnimatorOverrideController animatorController;
    public SkillBook skillBook;

    [Header("Inintialize value")]
    public float startAttck;//공격력 초기값
    public float startDefence;//방어력 초기값
    public float startMaxHealth;//체력 초기값
    public float startMaxMana;
    public float startCritical;//치명타 초기값
    public float startAccurracy;
    public float startEvasion;
    public float startSpeed;

    [Header("Levelup value")]
    public float levelAttck;//공격력 초기값
    public float levelDefence;//방어력 초기값
    public float levelMaxHealth;//체력 초기값
    public float levelMaxMana;
    public float levelCritical;//치명타 초기값
    public float levelAccurracy;
    public float leveltEvasion;
    public float levelSpeed;
}
