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
        //도사 , 검객, 유생, 스님, 사냥꾼, 기생
        int number = SynergyCheaker.Where(element => true).Count();
        if(number == 1)
        {
            Debug.Log("같은 타입이 4개");

            for (int i = 0; i< party.Length; i++) 
            {
                //party[i].getComponent<Player>().스탯업
            }
        }

        if (SynergyCheaker[2] && SynergyCheaker[5]) //유생, 기생
        {
            Debug.Log("매난국죽");
            Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.Confusianism);
            Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.KiSeng);

            //시너지 아이콘 업
        }

        if (SynergyCheaker[1] && SynergyCheaker[4]) //사냥꾼 검객
        {
            Debug.Log("몸 쓰는 사람들");
            Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.KumGeok);
            Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.Hunter);
            //시너지 아이콘 업
        }

        if (SynergyCheaker[2] && SynergyCheaker[3]) //스님, 유생
        {
            Debug.Log("종교전쟁");
            Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.Confusianism);
            Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.Buddhist);
            //시너지 아이콘 업
        }

        if (SynergyCheaker[0] && SynergyCheaker[5])
        {
            Debug.Log("한국판 하울의 움직이는 성");
            Player[] Confu = Array.FindAll(party, element => element.playerType == PlayerType.Dosa);
            Player[] Kiseng = Array.FindAll(party, element => element.playerType == PlayerType.KiSeng);
            //시너지 아이콘 업
        }



    }




}
