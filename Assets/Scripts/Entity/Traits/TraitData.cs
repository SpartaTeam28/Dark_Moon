using UnityEngine;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Traits/Trait")]
public class TraitData : ScriptableObject
{
    public string traitName;
    [TextArea] public string description;

    public StatType affectedStat;
    public float valueModifier;      // 고정 증가/감소
    public float multiplierModifier; // 배율 증가/감소

    public TraitData Clone()
    {
        return new TraitData { 
            traitName = this.traitName, description = this.description, affectedStat = this.affectedStat,
            valueModifier = this.valueModifier, multiplierModifier = this.multiplierModifier};
    }
}