using System;
using UnityEngine;

public class BaseStat : MonoBehaviour
{
    public float value;
    public float valueMultiples = 1f;

    public event Action<float> update;

    public virtual void SetStat(float amount)
    {
        if (amount < 0) return;
        value = amount;
    }

    public virtual void Init(float amount)
    {
        SetStat(amount);
    }

    public virtual float GetStat()
    {
        return value * valueMultiples;
    }

    public void AddStat(float amount)
    {
        SetStat(amount + value);
    }

    public void SubStat(float amount)
    {
        SetStat(Mathf.Max(0, value - amount));
    }

    public void SetStatMultiples(float amount)
    {
        if (amount < 0) return;
        valueMultiples = amount;
    }

    public void AddMultiples(float amount)
    {
        SetStatMultiples(amount + valueMultiples);
    }

    public void SubMultiples(float amount)
    {
        SetStatMultiples(Mathf.Max(0, valueMultiples - amount));
    }

    public virtual string GetValueToString() 
    {
        return (value * valueMultiples).ToString("F2");
    }
}
