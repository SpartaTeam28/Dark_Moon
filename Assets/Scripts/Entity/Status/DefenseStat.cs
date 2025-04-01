public class DefenseStat : BaseStat
{
    public override void SetStat(float amount)
    {
        base.SetStat(amount);
        job.defence = value;
    }
}