using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStat stat;

    private void Awake()
    {
        stat = GetComponent<CharacterStat>();
    }
}
