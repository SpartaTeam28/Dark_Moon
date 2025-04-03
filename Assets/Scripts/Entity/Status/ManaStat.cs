using UnityEngine;

public class ManaStat : BaseStat
{
    public float curMana;

    public override void Init(float amount)
    {
        base.Init(amount);
        curMana = value * valueMultiples;
    }

    public override void SetStat(float amount)
    {
        base.SetStat(amount);
        if (value == 0)
        {
            value = 1;
        }
        if (value * valueMultiples < curMana)
        {
            curMana = value * valueMultiples;
        }
    }

    public void SetMana(float amount)
    {
        if (value * valueMultiples < curMana)
        {
            curMana = value * valueMultiples;
        }
        else
        {
            curMana = amount;
        }
    }

    public float GetMana()
    {
        return curMana;
    }

    public void AddHealth(float amount)
    {
        SetMana(Mathf.Max(0, curMana + amount));
    }

    public void FullMana()
    {
        curMana = value;
    }

    public override string GetValueToString()
    {
        return curMana.ToString("F0") + "/" + base.GetValueToString();
    }
}
