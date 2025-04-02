using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterTraitManager : MonoBehaviour
{
    [Header("��� ������ Ư����")]
    public List<TraitData> availableTraits;

    [Header("����� Ư��")]
    public List<TraitData> appliedTraits = new List<TraitData>();

    [Header("Ư�� �ִ� ����")]
    [SerializeField] private int maxTraitCount = 2;

    private CharacterStat stat;

    private void Awake()
    {
        stat = GetComponent<CharacterStat>();
        availableTraits = GameManager.instance.availableTraits.Select(trait => trait.Clone()).ToList();
    }

    private void Start()
    {
        ApplyRandomTraits(maxTraitCount);
    }

    public void ApplyTaritReroll()
    {
        AppliedTriatsClear();
        ApplyRandomTraits(maxTraitCount);
    }

    public void AppliedTriatsClear()
    {
        foreach (var trait in appliedTraits)
        {
            BaseStat oldStat = stat.StatusDictionary[trait.affectedStat];
            if (trait.valueModifier != 0f)
                oldStat.AddStat(trait.valueModifier * -1);

            if (trait.multiplierModifier != 0f)
                oldStat.AddStat(trait.multiplierModifier * -1);
            availableTraits.Add(trait);
        }
        appliedTraits.Clear();
    }

    public void ApplyRandomTraits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (appliedTraits.Count >= maxTraitCount)
            {
                Debug.LogWarning("Ư�� �ִ� ���� ����!");
                return;
            }

            if (availableTraits.Count == 0) return;

            TraitData trait = availableTraits[Random.Range(0, availableTraits.Count)];
            ApplyTrait(trait);
            availableTraits.Remove(trait);
        }
    }

    public void ApplyTrait(TraitData trait)
    {
        if(appliedTraits.Count >= maxTraitCount)
        {
            Debug.LogWarning("�� �̻� Ư���� �߰��� �� �����ϴ�!");
            return;
        }

        if (trait == null || stat == null) return;

        BaseStat targetStat = stat.StatusDictionary[trait.affectedStat];

        if (trait.valueModifier != 0f)
            targetStat.AddStat(trait.valueModifier);

        if (trait.multiplierModifier != 0f)
            targetStat.AddMultiples(trait.multiplierModifier);

        appliedTraits.Add(trait);

        Debug.Log($"[Ư�� ����] {trait.traitName}: {trait.description}");
    }

    public void IncreaseTraitLimit(int amount = 1)
    {
        maxTraitCount += amount;
        Debug.Log($"Ư�� ������ Ȯ��Ǿ����ϴ�! ���� �ִ�: {maxTraitCount}");
    }

    public void ReplaceTrait(TraitData oldTrait)
    {
        if (!appliedTraits.Contains(oldTrait))
        {
            Debug.LogWarning("�� Ư���� ���� ����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        int oldIndex = appliedTraits.IndexOf(oldTrait);

        // ���� Ư�� ȿ�� ����
        BaseStat oldStat = stat.StatusDictionary[oldTrait.affectedStat];

        if (oldTrait.valueModifier != 0f)
            oldStat.AddStat(oldTrait.valueModifier * -1);

        if (oldTrait.multiplierModifier != 0f)
            oldStat.AddStat(oldTrait.multiplierModifier * -1);

        appliedTraits.RemoveAt(oldIndex);
        availableTraits.Add(oldTrait); // ������ Ư���� �ٽ� Ǯ�� �ǵ���

        // ���ο� Ư�� ������ ����
        if (availableTraits.Count == 0)
        {
            Debug.LogWarning("�� �̻� �ο��� �� �ִ� Ư���� �����ϴ�.");
            return;
        }

        TraitData newTrait = availableTraits[Random.Range(0, availableTraits.Count)];

        // �� Ư�� ����
        BaseStat newStat = stat.StatusDictionary[newTrait.affectedStat];

        if (newTrait.valueModifier != 0f)
            newStat.AddStat(newTrait.valueModifier);

        if (newTrait.multiplierModifier != 0f)
            newStat.AddMultiples(newTrait.multiplierModifier);

        appliedTraits.Insert(oldIndex, newTrait);
        availableTraits.Remove(newTrait); // �ߺ� ����

        Debug.Log($"[Ư�� ��ü] {oldTrait.traitName} �� {newTrait.traitName}");
    }
}