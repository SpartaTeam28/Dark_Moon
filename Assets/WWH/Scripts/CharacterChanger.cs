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

        for(int i  = 0; i < transform.childCount; i++) 
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0;i < GameManager.instance.friendlyCharacterList.Count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);

            BattleCharacterList.Add(transform.GetChild(i).GetComponent<Character>());
            BattleCharacterList[i].info = GameManager.instance.friendlyCharacterList[i].info;
            BattleCharacterList[i].GetComponent<Character>().stat = GameManager.instance.friendlyCharacterList[i].stat;

            BattleCharacterList[i].GetComponent<Player>().playerType = GameManager.instance.friendlyCharacterList[i].GetComponent<Player>().playerType;
        }

    }


}
