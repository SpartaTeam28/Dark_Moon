using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [Header("Inintialize value")]
    [SerializeField] private float startAttck;//���ݷ� �ʱⰪ
    [SerializeField] private float startDefence;//���� �ʱⰪ
    [SerializeField] private float startMaxHealth;//ü�� �ʱⰪ
    [SerializeField] private float startMaxMana;
    [SerializeField] private float startCritical;//ġ��Ÿ �ʱⰪ
    [SerializeField] private float startAccurracy;
    [SerializeField] private float startEvasion;
    [SerializeField] private float startSpeed;

    public AttackStat attack;//���ݷ�
    public DefenseStat defence;//����
    public HealthStat health;//ü��
    public ManaStat mana;
    public CriticalStat critical;//ġ��Ÿ
    public EvasionStat evasion;
    public AccuracyStat accuracy;
    public SpeedStat speed;

    public Dictionary<StatType, BaseStat> StatusDictionary { get; private set; }//StatType�� BaseStatus����


    private void Awake()
    {
        attack = AddAndInit<AttackStat>(startAttck);
        defence = AddAndInit<DefenseStat>(startDefence);
        health = AddAndInit<HealthStat>(startMaxHealth);
        mana = AddAndInit<ManaStat>(startMaxMana);
        critical = AddAndInit<CriticalStat>(startCritical);
        evasion = AddAndInit<EvasionStat>(startEvasion);
        accuracy = AddAndInit<AccuracyStat>(startAccurracy);
        speed = AddAndInit<SpeedStat>(startSpeed);

        StatusDictionary = new Dictionary<StatType, BaseStat>
        {
            { StatType.Attack, attack },
            { StatType.Defence, defence },
            { StatType.Health, health },
            { StatType.Mana, mana },
            { StatType.Critical, critical },
            { StatType.Accuracy, accuracy },
            { StatType.Evasion, evasion },
            { StatType.Speed, speed },
        };
    }

    private T AddAndInit<T>(float initValue) where T : BaseStat//���۳�Ʈ�� �߰� �ϰ� �ʱ�ȭ
    {
        T status = gameObject.AddComponent<T>();
        status.Init(initValue);
        return status;
    }
}
