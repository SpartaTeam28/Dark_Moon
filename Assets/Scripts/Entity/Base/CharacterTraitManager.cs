using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterTraitManager : MonoBehaviour
{
    [Header("사용 가능한 특성들")]
    public List<TraitData> availableTraits;

    [Header("적용된 특성")]
    public List<TraitData> appliedTraits = new List<TraitData>();

    [Header("특성 최대 갯수")]
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
                Debug.LogWarning("특성 최대 갯수 도달!");
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
            Debug.LogWarning("더 이상 특성을 추가할 수 없습니다!");
            return;
        }

        if (trait == null || stat == null) return;

        BaseStat targetStat = stat.StatusDictionary[trait.affectedStat];

        if (trait.valueModifier != 0f)
            targetStat.AddStat(trait.valueModifier);

        if (trait.multiplierModifier != 0f)
            targetStat.AddMultiples(trait.multiplierModifier);

        appliedTraits.Add(trait);

        Debug.Log($"[특성 적용] {trait.traitName}: {trait.description}");
    }

    public void IncreaseTraitLimit(int amount = 1)
    {
        maxTraitCount += amount;
        Debug.Log($"특성 슬롯이 확장되었습니다! 현재 최대: {maxTraitCount}");
    }

    public void ReplaceTrait(TraitData oldTrait)
    {
        if (!appliedTraits.Contains(oldTrait))
        {
            Debug.LogWarning("이 특성은 현재 적용되어 있지 않습니다.");
            return;
        }

        int oldIndex = appliedTraits.IndexOf(oldTrait);

        // 기존 특성 효과 제거
        BaseStat oldStat = stat.StatusDictionary[oldTrait.affectedStat];

        if (oldTrait.valueModifier != 0f)
            oldStat.AddStat(oldTrait.valueModifier * -1);

        if (oldTrait.multiplierModifier != 0f)
            oldStat.AddStat(oldTrait.multiplierModifier * -1);

        appliedTraits.RemoveAt(oldIndex);
        availableTraits.Add(oldTrait); // 제거한 특성은 다시 풀로 되돌림

        // 새로운 특성 무작위 선택
        if (availableTraits.Count == 0)
        {
            Debug.LogWarning("더 이상 부여할 수 있는 특성이 없습니다.");
            return;
        }

        TraitData newTrait = availableTraits[Random.Range(0, availableTraits.Count)];

        // 새 특성 적용
        BaseStat newStat = stat.StatusDictionary[newTrait.affectedStat];

        if (newTrait.valueModifier != 0f)
            newStat.AddStat(newTrait.valueModifier);

        if (newTrait.multiplierModifier != 0f)
            newStat.AddMultiples(newTrait.multiplierModifier);

        appliedTraits.Insert(oldIndex, newTrait);
        availableTraits.Remove(newTrait); // 중복 방지

        Debug.Log($"[특성 교체] {oldTrait.traitName} → {newTrait.traitName}");
    }
}