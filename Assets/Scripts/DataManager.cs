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
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //go.GetComponent<Character>().info.job = friendlyCharacters[0].baseJob;
    }
    private void Init()
    {
        for (int i = 0; i < friendlyCharacterNum; i++)
        {
            CharacterInstance ci = new CharacterInstance();
            ci.baseJob = jobs[Random.Range(0, jobs.Count)].Clone();
            friendlyCharacters.Add(ci);
        }
        go.GetComponent<Character>().info.job = friendlyCharacters[0].baseJob;
    }
}
