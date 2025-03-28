using UnityEngine;

public class CriticalStat : BaseStat
{
    public float criticalDamageMutilples = 1.5f;

    public override void SetStat(float amount)
    {
        base.SetStat(amount);
        if(value > 100)
        {
            value = 100f;
        }
    }

    public void SetDamageMultiples(float amount)
    {
        if (amount < 0) return;
        criticalDamageMutilples = amount;
    }

    public void AddDamageMultiples(float amount)
    {
        SetDamageMultiples(criticalDamageMutilples + amount);
    }

    public void SubDamageMultiples(float amount)
    {
        SetDamageMultiples(Mathf.Max(0, criticalDamageMutilples + amount));
    }
}
