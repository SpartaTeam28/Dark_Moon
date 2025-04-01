using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public Player playerCharacter;
    public List<Character> friendlyCharacterList;
    public List<Character> EnemyCharacterList;

    public List<TraitData> availableTraits;

    public NameGenerator nameGenerator;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            nameGenerator = gameObject.AddComponent<NameGenerator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
