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
    [SerializeField] private float startCritical;//ġ��Ÿ �ʱⰪ
    [SerializeField] private float startAccurracy;
    [SerializeField] private float startEvasion;

    public AttackStat attack;//���ݷ�
    public DefenseStat defence;//����
    public HealthStat health;//ü��
    public CriticalStat critical;//ġ��Ÿ
    public EvasionStat evasion;
    public AccuracyStat accuracy;

    public Dictionary<StatType, BaseStat> StatusDictionary { get; private set; }//StatType�� BaseStatus����


    private void Awake()
    {
        attack = AddAndInit<AttackStat>(startAttck);
        defence = AddAndInit<DefenseStat>(startDefence);
        health = AddAndInit<HealthStat>(startMaxHealth);
        critical = AddAndInit<CriticalStat>(startCritical);
        evasion = AddAndInit<EvasionStat>(startEvasion);
        accuracy = AddAndInit<AccuracyStat>(startAccurracy);

        StatusDictionary = new Dictionary<StatType, BaseStat>
        {
            { StatType.Attack, attack },
            { StatType.Defence, defence },
            { StatType.Health, health },
            { StatType.Critical, critical },
            { StatType.Accuracy, accuracy },
            { StatType.Evasion, evasion },
        };
    }

    private T AddAndInit<T>(float initValue) where T : BaseStat//���۳�Ʈ�� �߰� �ϰ� �ʱ�ȭ
    {
        T status = gameObject.AddComponent<T>();
        status.Init(initValue);
        return status;
    }
}
