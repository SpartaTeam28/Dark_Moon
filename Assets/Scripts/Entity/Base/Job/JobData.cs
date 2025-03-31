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
    public float startAttck;//���ݷ� �ʱⰪ
    public float startDefence;//���� �ʱⰪ
    public float startMaxHealth;//ü�� �ʱⰪ
    public float startMaxMana;
    public float startCritical;//ġ��Ÿ �ʱⰪ
    public float startAccurracy;
    public float startEvasion;
    public float startSpeed;

    [Header("Levelup value")]
    public float levelAttck;//���ݷ� �ʱⰪ
    public float levelDefence;//���� �ʱⰪ
    public float levelMaxHealth;//ü�� �ʱⰪ
    public float levelMaxMana;
    public float levelCritical;//ġ��Ÿ �ʱⰪ
    public float levelAccurracy;
    public float leveltEvasion;
    public float levelSpeed;
}
