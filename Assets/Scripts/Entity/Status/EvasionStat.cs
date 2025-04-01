public class EvasionStat : BaseStat
{
    public override void SetStat(float amount)
    {
        base.SetStat(amount);
        if (value > 100)
        {
            value = 100f;
        }
        job.evasion = value;
    }
}
