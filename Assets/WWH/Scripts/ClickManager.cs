using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

    private static ClickManager instance;
    public static ClickManager Instance { get { return instance; } set { instance = value; } }


    public SkillBook skillBook;

    public SKilldata sKilldata;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetSkill(int index)
    {
        sKilldata = skillBook.SilhumSkill[index];
    }


    


    
  
}
