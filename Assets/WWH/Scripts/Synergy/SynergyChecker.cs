using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SynergyChecker : MonoBehaviour
{
    public bool[] SynergyCheckerList = new bool[] { false, false, false,false,false,false };

    public GameObject SynergyPrefabs;
    public Sprite[] SynergyIcon;
    public SynergyData[] synergyDatas;


    public Dictionary<PlayerType, List< Character>> characterList = new Dictionary<PlayerType, List< Character>>();

    public List<Character> confu;
    public List<Character> kiseng;
    public List<Character> kumGeok;
    public List<Character> buddi;
    public List<Character> hunter;
    public List<Character> doSa;

    private void Awake()
    {
        ListInit();
        ResourceLoad();
        PartySynergyCheck();

    }



    private void Start()
    {
        PartySynergyOn();
    }

    public void ResourceLoad()
    {
        synergyDatas = Resources.LoadAll<SynergyData>("WWHSynergyData");
        SynergyPrefabs = Resources.Load<GameObject>("WWHICON/Synergy");
        SynergyIcon = Resources.LoadAll<Sprite>("WWHICON");
    }


    public void ListInit()
    {
        foreach(PlayerType type in characterList.Keys)
        {
            characterList[type] = new List<Character>();
        }
        
    }

    public void PartySynergyCheck()
    {
        foreach(Character cha in GameManager.instance.friendlyCharacterList)
        {
            if (characterList.ContainsKey(cha.GetComponent<Player>().playerType))
            {
                SynergyCheckerList[(int)cha.GetComponent<Player>().playerType] = true;
                characterList[cha.GetComponent<Player>().playerType].Add(cha);

            }
        }
    }

    public void PartySynergyOn()
    {
        //µµ»ç , °Ë°´, À¯»ý, ½º´Ô, »ç³É²Û, ±â»ý
        int number = SynergyCheckerList.Where(element => element == true).Count();
        Debug.Log(number);
        if(number == 1)
        {
            for (int i = 0; i< GameManager.instance.friendlyCharacterList.Count; i++) 
            {
                GameManager.instance.friendlyCharacterList[i].GetComponent<CharacterStat>().health.AddHealth(100f);
            }
            SetSynergyPopUP(0);
        }

        TwoCharacterSynergy(PlayerType.KiSeng, PlayerType.Confusianism, 1);
        TwoCharacterSynergy(PlayerType.Hunter, PlayerType.KumGeok, 2);
        TwoCharacterSynergy(PlayerType.Buddhist, PlayerType.Confusianism, 3);
        TwoCharacterSynergy(PlayerType.Dosa, PlayerType.KiSeng, 4);

    }

    public void TwoCharacterSynergy(PlayerType type, PlayerType type1,int index )
    {
        if (SynergyCheckerList[(int)type] && SynergyCheckerList[(int)type1])
        {
            List<List<Character>> synergyGroups = new List<List<Character>> { characterList[type1], characterList[type] };
            SetSynergyPopUP(index);
            ApplySynergy(synergyGroups, synergyDatas[index]);
        }
    }

    public void SetSynergyPopUP(int index)
    {
        GameObject Popup = Instantiate(SynergyPrefabs, transform);
        Popup.GetComponent<Synergy>().SetIcon(SynergyIcon[index]);
        Popup.GetComponent<Synergy>().SetText(synergyDatas[index]);
    }
    public void ApplySynergy(List<List<Character>> Jobset, SynergyData synergyData)
    {


        if(!synergyData.IsAdd)
        {
            foreach(List<Character> characterList in Jobset)
            {
                foreach(Character character in characterList)
                {
                    character.GetComponent<CharacterStat>().StatusDictionary[synergyData.statType].AddMultiples(synergyData.multivalue);
                }
            }
        }
        else
        {
            foreach (List<Character> characterList in Jobset)
            {
                foreach (Character character in characterList)
                {
                    character.GetComponent<CharacterStat>().StatusDictionary[synergyData.statType].AddStat(synergyData.value);
                }
            }
        }
    }

    public void EndSynergy(List<List<Character>> Jobset, SynergyData synergyData)
    {
        if (!synergyData.IsAdd)
        {
            foreach (List<Character> characterList in Jobset)
            {
                foreach (Character character in characterList)
                {
                    character.GetComponent<CharacterStat>().StatusDictionary[synergyData.statType].AddMultiples(-synergyData.multivalue);
                }
            }
        }
        else
        {
            foreach (List<Character> characterList in Jobset)
            {
                foreach (Character character in characterList)
                {
                    character.GetComponent<CharacterStat>().StatusDictionary[synergyData.statType].AddStat(-synergyData.value);
                }
            }
        }
    }




}
