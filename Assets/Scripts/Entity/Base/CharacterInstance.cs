using System.Collections.Generic;

[System.Serializable]
public class CharacterInstance
{
    public JobData baseJob;
    public List<TraitData> traits = new List<TraitData>();
}