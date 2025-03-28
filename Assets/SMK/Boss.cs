using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    int hp;
    int atk = 20;
    int speed = 20;
    int maxHp = 1000;
    GameObject bossPrefab;
    private bool bossSpawned = false;
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossSpawned)
        {
            SpawnBoss();
        }
    }

    public void Attack()
    {
        // GameManager.Instance.~ -= atk;
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab,new Vector3(5,0,0),Quaternion.identity);
        bossSpawned = true;
    }
}
