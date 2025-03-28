using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStat stat;
    public CharacterTraitManager traitManager;

    private void Awake()
    {
        stat = GetComponent<CharacterStat>();
        traitManager = GetComponent<CharacterTraitManager>();
    }

    public void TakeDamaged(float amount)
    {
        var health = stat.health;
        if(health == null)
        {
            Debug.LogError("Character dose not have Health Component.");
            return;
        }
        health.AddHealth(amount * -1);
        if(health.curHealth <= 0)
        {
            OnDie();
            return;
        }
    }

    private void OnDie()
    {

    }
}
