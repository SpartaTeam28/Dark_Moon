using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Synergy", menuName = "Scriptable/Synergy")]
public class SynergyData : ScriptableObject
{
    public string synergyName;
    public string synergyDes;
    public string synergyEffect;
    public StatType statType;
    public bool IsAdd;
    public int value;
    public float multivalue;
}
