using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    public List<JobData> jobs;
    public GameObject prefab;

    public List<GameObject> characters;
    public int createNum = 4;

    private void Start()
    {
        CreateCharacters();
    }

    public void CreateCharacters()
    {
        for (int i = 0; i < createNum; i++)
        {
            //GameObject go = Instantiate(prefab);
            //go.GetComponent<Character>().info.job = jobs[Random.Range(0, jobs.Count)].Clone();
            //go.GetComponent<CharacterInfo>().Init();
            //go.GetComponent<CharacterStat>().Init();
            //go.GetComponent<Character>().trait.SetTraits(GameManager.instance.characterTraitManager.GetRandomTraits(2));
            
            //characters.Add(go);
            //DataManager.instance.AddCharacter(go.GetComponent<Character>());
            characters[i].GetComponent<Character>().info.job = jobs[Random.Range(0, jobs.Count)].Clone();
            characters[i].GetComponent<CharacterInfo>().Init();
            characters[i].GetComponent<CharacterStat>().Init();
            characters[i].GetComponent<Character>().trait.SetTraits(GameManager.instance.characterTraitManager.GetRandomTraits(2));
            DataManager.instance.AddCharacter(characters[i].GetComponent<Character>());
        }
    }
}
