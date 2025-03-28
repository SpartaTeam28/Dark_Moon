using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SynergyChecker : MonoBehaviour
{

    public Player[] party = new Player[4];

    public bool[] SynergyCheaker = new bool[] { false, false, false,false,false,false };

    public GameObject SynergyPrefabs;
    public Sprite[] SynergyIcon;
    public SynergyData[] synergyDatas;

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
        if (Array.Find(party, element => element.playerType == PlayerType.Dosa))
            SynergyCheaker[0] = true;
        if (Array.Find(party, element => element.playerType == PlayerType.KumGeok))
            SynergyCheaker[1] = true;
        if (Array.Find(party, element => element.playerType == PlayerType.Confusianism))
            SynergyCheaker[2] = true;
        if (Array.Find(party, element => element.playerType == PlayerType.Buddhist))
            SynergyCheaker[3] = true;
        if (Array.Find(party, element => element.playerType == PlayerType.Hunter))
            SynergyCheaker[4] = true;
        if (Array.Find(party, element => element.playerType == PlayerType.KiSeng))
            SynergyCheaker[5] = true;
    }

    public void PartySynergyOn()
    {
        //���� , �˰�, ����, ����, ��ɲ�, ���
        int number = SynergyCheaker.Where(element => true).Count();
        if(number == 1)
        {
            for (int i = 0; i< party.Length; i++) 
            {
                party[i].GetComponent<CharacterStat>().health.AddHealth(100f);
            }
            SetSynergyPopUP(0);
        }

        if (SynergyCheaker[2] && SynergyCheaker[5]) //����, ���
        {


            Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.Confusianism);
            Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.KiSeng);
            Player[][] Jobset = new Player[][] {Confu, Kiseng};

            SetSynergyPopUP(1);
            ApplySynergy(Jobset, synergyDatas[1]);

        }

        //if (SynergyCheaker[1] && SynergyCheaker[4]) //��ɲ� �˰�
        //{
        //    Debug.Log("�� ���� �����");
        //    Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.KumGeok);
        //    Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.Hunter);
        //    //�ó��� ������ ��
        //}

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
    public void ApplySynergy(Player[][] Jobset, SynergyData synergyData)
    {

        if (synergyData.IsAdd)
        {
            for (int i = 0; i < Jobset.Length; i++)
            {
                for (int j = 0; j < Jobset[i].Length; j++)
                {
                    Jobset[i][j].GetComponent<CharacterStat>().StatusDictionary[synergyData.statType].AddStat(synergyData.value);
                }
            }
        }
        else
        {
            for (int i = 0; i < Jobset.Length; i++)
            {
                for (int j = 0; j < Jobset[i].Length; j++)
                {
                    Jobset[i][j].GetComponent<CharacterStat>().StatusDictionary[synergyData.statType].AddMultiples(synergyData.multivalue);
                }
            }
        }
    

       
    }




}
