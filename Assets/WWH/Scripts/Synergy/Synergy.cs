using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Synergy : MonoBehaviour
{

    public Player[] party = new Player[4];

    public bool[] SynergyCheaker = new bool[] { false, false, false,false,false,false };


    private void Awake()
    {
        PartySynergyCheck();
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
            Debug.Log("���� Ÿ���� 4��");

            for (int i = 0; i< party.Length; i++) 
            {
                //party[i].getComponent<Player>().���Ⱦ�
            }
        }

        if (SynergyCheaker[2] && SynergyCheaker[5]) //����, ���
        {
            Debug.Log("�ų�����");
            Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.Confusianism);
            Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.KiSeng);

            //�ó��� ������ ��
        }

        if (SynergyCheaker[1] && SynergyCheaker[4]) //��ɲ� �˰�
        {
            Debug.Log("�� ���� �����");
            Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.KumGeok);
            Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.Hunter);
            //�ó��� ������ ��
        }

        if (SynergyCheaker[2] && SynergyCheaker[3]) //����, ����
        {
            Debug.Log("��������");
            Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.Confusianism);
            Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.Buddhist);
            //�ó��� ������ ��
        }

        if (SynergyCheaker[0] && SynergyCheaker[5])
        {
            Debug.Log("�ѱ��� �Ͽ��� �����̴� ��");
            Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.Dosa);
            Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.KiSeng);
            //�ó��� ������ ��
        }



    }




}
