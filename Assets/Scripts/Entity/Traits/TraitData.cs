using UnityEngine;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Traits/Trait")]
public class TraitData : ScriptableObject
{
    public string traitName;
    [TextArea] public string description;

    public StatType affectedStat;
    public float valueModifier;      // ���� ����/����
    public float multiplierModifier; // ���� ����/����
}