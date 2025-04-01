using System.Collections.Generic;
using UnityEngine;

public class CharacterTraitManager : MonoBehaviour
{
    [Header("사용 가능한 특성들")]
    public List<TraitData> availableTraits;

    public void ApplyTraitEffects(CharacterTrait traitComponent, CharacterStat stat)
    {
        foreach (var trait in traitComponent.appliedTraits)
        {
            if (stat.StatusDictionary.TryGetValue(trait.affectedStat, out var targetStat))
            {
                if (targetStat == null)
                {
                    Debug.LogError($"[Trait 적용 실패] StatType '{trait.affectedStat}'의 BaseStat이 null입니다.");
                    continue;
                }

                if (trait.valueModifier != 0f)
                    targetStat.AddStat(trait.valueModifier);

                if (trait.multiplierModifier != 0f)
                    targetStat.AddMultiples(trait.multiplierModifier);
            }
        }
    }
    public void RemoveTraitEffects(CharacterTrait traitComponent, CharacterStat stat)
    {
        foreach (var trait in traitComponent.appliedTraits)
        {
            if (stat.StatusDictionary.TryGetValue(trait.affectedStat, out var targetStat))
            {
                if (trait.valueModifier != 0f)
                    targetStat.AddStat(-trait.valueModifier);

                if (trait.multiplierModifier != 0f)
                    targetStat.AddMultiples(-trait.multiplierModifier);
            }
        }
    }

    public List<TraitData> GetRandomTraits(int count, List<TraitData> excludeTraits = null)
    {
        List<TraitData> pool = new List<TraitData>(availableTraits);

        // 제외할 특성이 있다면 제거
        if (excludeTraits != null && excludeTraits.Count > 0)
        {
            pool.RemoveAll(t => excludeTraits.Contains(t));
        }

        List<TraitData> result = new List<TraitData>();

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            var trait = pool[Random.Range(0, pool.Count)];
            result.Add(trait);
            pool.Remove(trait);
        }

        return result;
    }
}