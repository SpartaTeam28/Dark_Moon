using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{

    List<Character> BattleCharacterList = new List<Character>();

    private void Awake()
    {
        Set();
    }


    public void Set()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BattleCharacterList.Add(transform.GetChild(i).GetComponent<Character>());
        }

        for (int i = 0;i < BattleCharacterList.Count; i++)
        {
            BattleCharacterList[i].info.job = GameManager.instance.friendlyCharacterList[i].info.job;
            BattleCharacterList[i].GetComponent<Player>().playerType = GameManager.instance.friendlyCharacterList[i].GetComponent<Player>().playerType;
        }

    }


}
