using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public Player playerCharacter;
    public List<Character> characterList;
    public List<Character> friendlyCharacterList;
    public List<Character> EnemyCharacterList;

    public List<TraitData> availableTraits;

    public NameGenerator nameGenerator;
    
    public CharacterTraitManager characterTraitManager;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            nameGenerator = gameObject.AddComponent<NameGenerator>();
            characterTraitManager = gameObject.GetComponent<CharacterTraitManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
