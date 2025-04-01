using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public static DataManager Instance { get { return instance; } }

    public List<JobData> jobs;

    public List<CharacterInstance> friendlyCharacters = new List<CharacterInstance>();

    public int friendlyCharacterNum;

    public GameObject go;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        for (int i = 0; i < friendlyCharacterNum; i++)
        {
            CharacterInstance ci = new CharacterInstance();
            ci.baseJob = jobs[Random.Range(0, jobs.Count)].Clone();
            ci.traits = GameManager.instance.characterTraitManager.GetRandomTraits(2);
            friendlyCharacters.Add(ci);
        }
        go.GetComponent<Character>().info.job = friendlyCharacters[0].baseJob;
        go.GetComponent<Character>().trait.SetTraits(friendlyCharacters[0].traits);
    }

    public void AddCharacter(Character character)
    {
        if (character == null) return;
        if(friendlyCharacters.Count > friendlyCharacterNum) return;

        CharacterInstance ci = new CharacterInstance();
        ci.baseJob = character.info.job;
        ci.traits = character.trait.appliedTraits;
        friendlyCharacters.Add(ci);

        //Character character1 = new Character();
        //character.info.job = ci.baseJob;
        //character.info.Init();
        //character1.stat.Init();
        //character1.trait.appliedTraits = ci.traits;
    }
    
}
