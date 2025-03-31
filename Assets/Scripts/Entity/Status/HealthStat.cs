using Unity.VisualScripting;
using UnityEngine;

public class HealthStat : BaseStat
{
    public float curHealth;

    public override void Init(float amount)
    {
        base.Init(amount);
        curHealth = value * valueMultiples;
    }

    public override void SetStat(float amount)
    {
        base.SetStat(amount);
        if(value == 0)
        {
            value = 1;
        }
        if (value * valueMultiples < curHealth)
        {
            curHealth = value * valueMultiples;
        }
    }

    public void SetHealth(float amount)
    {
        if (value * valueMultiples < curHealth)
        {
            curHealth = value * valueMultiples;
        }
        else
        {
            curHealth = amount;
        }
    }

    public float GetHealth()
    {
        return curHealth;
    }

    public void AddHealth(float amount)
    {
        SetHealth(Mathf.Max(0, curHealth + amount));
    }

    public void FullHealth()
    {
        curHealth = value;
    }

    public override string GetValueToString()
    {
        return curHealth.ToString("F2") + "/" + base.GetValueToString();
    }
}