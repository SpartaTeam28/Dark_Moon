using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    int hp;
    int atk = 20; // 공격력
    int speed = 20; // 공격속도
    int maxHp = 1000; 
    public GameObject bossPrefab; // bossPrefab
    private bool bossSpawned = false;
    bool isDead = false;
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Die();
        }
    }

   /* void SpawnBoss() // 보스 생성
    {
        Instantiate(bossPrefab,new Vector3(5,0,0),Quaternion.identity);
        bossSpawned = true;
    }
   */
    void Die()
    {
        if (hp <= 0)
        {
            StageManager.Instance.isMonsterDie = true;
            StageManager.Instance.ChangeScene();
           
        }
        
    }
    public void Attack() // 보스가 플레이어 공격 ,타격
    {
        // GameManager.Instance.~ -= atk;
    }
    public void TakeDamage(int damage) // 피격
    {
        hp -= damage;
    }
}
