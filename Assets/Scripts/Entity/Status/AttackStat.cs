public class AttackStat : BaseStat
{
    public override void SetStat(float amount)
    {
        base.SetStat(amount);
        job.attack = value;
    }
}