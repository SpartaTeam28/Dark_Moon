using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [Header("Inintialize value")]
    [SerializeField] private float startAttck;//공격력 초기값
    [SerializeField] private float startDefence;//방어력 초기값
    [SerializeField] private float startMaxHealth;//체력 초기값
    [SerializeField] private float startCritical;//치명타 초기값
    [SerializeField] private float startAccurracy;
    [SerializeField] private float startEvasion;

    public AttackStat attack;//공격력
    public DefenseStat defence;//방어력
    public HealthStat health;//체력
    public CriticalStat critical;//치명타
    public EvasionStat evasion;
    public AccuracyStat accuracy;

    public Dictionary<StatType, BaseStat> StatusDictionary { get; private set; }//StatType과 BaseStatus연결


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

    private T AddAndInit<T>(float initValue) where T : BaseStat//컴퍼넌트를 추가 하고 초기화
    {
        T status = gameObject.AddComponent<T>();
        status.Init(initValue);
        return status;
    }
}
