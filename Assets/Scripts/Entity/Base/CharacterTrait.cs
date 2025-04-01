using System.Collections.Generic;
using UnityEngine;

public class CharacterTrait : MonoBehaviour
{
    public CharacterStat stat;

    [Header("����� Ư��")]
    public List<TraitData> appliedTraits = new List<TraitData>();

    [Header("Ư�� �ִ� ����")]
    [SerializeField] private int maxTraitCount = 2;

    public int MaxTraitCount => maxTraitCount;

    public bool CanAddTrait => appliedTraits.Count < maxTraitCount;

    public void IncreaseTraitLimit(int amount = 1)
    {
        maxTraitCount += amount;
    }

    public void SetTraits(List<TraitData> newTraits)
    {
        appliedTraits = new List<TraitData>(newTraits);
        GameManager.instance.characterTraitManager.ApplyTraitEffects(this, stat);
    }

    public void ClearTraits()
    {
        GameManager.instance.characterTraitManager.RemoveTraitEffects(this, stat);
        appliedTraits.Clear();
    }

    public void RerollTrait()
    {
        ClearTraits();
        SetTraits(GameManager.instance.characterTraitManager.GetRandomTraits(maxTraitCount));
    }
}
