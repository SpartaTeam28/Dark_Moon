using System;
using UnityEngine;


public class SkillBook : MonoBehaviour
{
    private SKilldata[] allSkillSet;
    public SKilldata[] skillList;
    private void Start()
    {
        GetSkill();
    }
    public SKilldata[] FourSkillSet(string PlayerName)
    {
        allSkillSet = Resources.LoadAll<SKilldata>($"WWHSkill/{PlayerName}");
        SKilldata[] returnSkill = new SKilldata[4];
        returnSkill[0] = allSkillSet[0];

        Shuffle(allSkillSet);

        for (int i = 1; i <4; i++) 
        {
            returnSkill[i] = allSkillSet[i];
        }
        return returnSkill;
    }

    public void GetSkill()
    {
        switch (transform.GetComponent<Player>().playerType)
        {
            case PlayerType.Dosa:
                skillList = FourSkillSet("Dosa");
                break;
            case PlayerType.KiSeng:
                skillList = FourSkillSet("GiSeng");
                break;
            case PlayerType.KumGeok:
                skillList = FourSkillSet("GumGeok");
                break;
            case PlayerType.Confusianism:
                skillList = FourSkillSet("Confucianism");
                break;
            case PlayerType.Buddhist:
                skillList = FourSkillSet("Buddhist");
                break;
            case PlayerType.Hunter:
                skillList = FourSkillSet("Hunter");
                break;
        }

 
    }

    public T[] Shuffle<T>(T[] Array)
    {

        for(int i = Array.Length-1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(1, i+1);

            T t = Array[j];
            Array[j] = Array[i];
            Array[i] = t;
        }
        return Array;
    }



}
