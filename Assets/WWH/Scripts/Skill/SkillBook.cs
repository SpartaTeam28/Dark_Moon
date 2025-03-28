using System;
using UnityEngine;


public class SkillBook : MonoBehaviour
{
    public SKilldata[] allSkillSet;
    public SKilldata[] SilhumSkill;
    private void Awake()
    {
        SilhumSkill =  FourSkillSet("Dosa");

    }
    public SKilldata[] FourSkillSet(string PlayerName)
    {
        allSkillSet = Resources.LoadAll<SKilldata>($"WWHSkill/{PlayerName}");
        Shuffle(allSkillSet);

        SKilldata[] returnSkill = new SKilldata[4];
        for (int i = 0; i <4; i++) 
        {
            returnSkill[i] = allSkillSet[i];
        }

        return returnSkill;

    }

    public T[] Shuffle<T>(T[] Array)
    {

        for(int i = Array.Length-1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i+1);

            T t = Array[j];
            Array[j] = Array[i];
            Array[i] = t;
        }
        return Array;
    }



}
