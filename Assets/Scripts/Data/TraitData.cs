using UnityEngine;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Traits/Trait")]
public class TraitData : ScriptableObject
{
    public string traitName;
    [TextArea] public string description;

    public StatType affectedStat;
    public float valueModifier;      // ���� ����/����
    public float multiplierModifier; // ���� ����/����

    public TraitData Clone()
    {
        return new TraitData { 
            traitName = this.traitName, description = this.description, affectedStat = this.affectedStat,
            valueModifier = this.valueModifier, multiplierModifier = this.multiplierModifier};
    }
}