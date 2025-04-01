using System.Collections.Generic;
using UnityEngine;

public class CharacterTraitManager : MonoBehaviour
{
    [Header("��� ������ Ư����")]
    public List<TraitData> availableTraits;

    public void ApplyTraitEffects(CharacterTrait traitComponent, CharacterStat stat)
    {
        foreach (var trait in traitComponent.appliedTraits)
        {
            if (stat.StatusDictionary.TryGetValue(trait.affectedStat, out var targetStat))
            {
                if (targetStat == null)
                {
                    Debug.LogError($"[Trait ���� ����] StatType '{trait.affectedStat}'�� BaseStat�� null�Դϴ�.");
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

        // ������ Ư���� �ִٸ� ����
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