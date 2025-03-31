using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SynergyChecker : MonoBehaviour
{





    public bool[] SynergyCheaker = new bool[] { false, false, false,false,false,false };

    public GameObject SynergyPrefabs;
    public Sprite[] SynergyIcon;
    public SynergyData[] synergyDatas;

    public List<Character> confu;
    public List<Character> kiseng;
    public List<Character> kumGeok;
    public List<Character> buddi;
    public List<Character> hunter;
    public List<Character> doSa;

    private void Awake()
    {
        PartySynergyCheck();
        synergyDatas = Resources.LoadAll<SynergyData>("WWHSynergyData");
        SynergyPrefabs = Resources.Load<GameObject>("WWHICON/Synergy");
        SynergyIcon = Resources.LoadAll<Sprite>("WWHICON");
    }



    private void Start()
    {
        PartySynergyOn();
    }


    public void PartySynergyCheck()
    {


        foreach(var character in GameManager.instance.friendlyCharacterList) 
        {
            if (character.GetComponent<Player>().playerType == PlayerType.Dosa)
            {
                SynergyCheaker[0] = true;
                doSa.Add(character);
                
            }

            if (character.GetComponent<Player>().playerType == PlayerType.KumGeok)
            {
                SynergyCheaker[1] = true;
                kumGeok.Add(character);
            }

            if (character.GetComponent<Player>().playerType == PlayerType.Confusianism)
            {
                SynergyCheaker[2] = true;
                confu.Add(character);
            }
     
            if (character.GetComponent<Player>().playerType == PlayerType.Buddhist)
            {
                SynergyCheaker[3] = true;
                buddi.Add(character);
            }

            if (character.GetComponent<Player>().playerType == PlayerType.Hunter)
            {
                SynergyCheaker[4] = true;
                hunter.Add(character);
            }

            if (character.GetComponent<Player>().playerType == PlayerType.KiSeng)
            {
                SynergyCheaker[5] = true;
                kiseng.Add(character);
            }
        }
    }

    public void PartySynergyOn()
    {
        //���� , �˰�, ����, ����, ��ɲ�, ���
        int number = SynergyCheaker.Where(element => true).Count();
        if(number == 1)
        {
            for (int i = 0; i< GameManager.instance.friendlyCharacterList.Count; i++) 
            {
                GameManager.instance.friendlyCharacterList[i].GetComponent<CharacterStat>().health.AddHealth(100f);
            }
            SetSynergyPopUP(0);
        }

        if (SynergyCheaker[2] && SynergyCheaker[5]) //����, ���
        {
            List<List<Character>> characters = new List<List<Character>>
            {
                confu,
                kiseng
            };
            SetSynergyPopUP(1);
            ApplySynergy(characters, synergyDatas[1]);
        }

        if (SynergyCheaker[1] && SynergyCheaker[4]) //��ɲ� �˰�
        {

            List<List<Character>> characters = new List<List<Character>>
            {
               kumGeok,
               hunter
            };
            SetSynergyPopUP(2);
            ApplySynergy(characters, synergyDatas[2]);
        }

        //if (SynergyCheaker[2] && SynergyCheaker[3]) //����, ����
        //{
        //    Debug.Log("��������");
        //    Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.Confusianism);
        //    Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.Buddhist);
        //    //�ó��� ������ ��
        //}

        //if (SynergyCheaker[0] && SynergyCheaker[5])
        //{
        //    Debug.Log("�ѱ��� �Ͽ��� �����̴� ��");
        //    Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.Dosa);
        //    Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.KiSeng);
        //    //�ó��� ������ ��
        //}



    }

    public void SetSynergyPopUP(int index)
    {
        GameObject Popup = Instantiate(SynergyPrefabs, transform);
        Popup.GetComponent<Synergy>().SetIcon(SynergyIcon[index]);
        Popup.GetComponent<Synergy>().SetText(synergyDatas[index]);
    }
    public void ApplySynergy(List<List<Character>> Jobset, SynergyData synergyData)
    {


        if(synergyData.IsAdd)
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
        if (synergyData.IsAdd)
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
