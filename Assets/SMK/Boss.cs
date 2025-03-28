using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float hp;
    public int atk = 20; // 공격력
    public int speed = 20; // 공격속도
    public int maxHp = 1000;
  

    [SerializeField] private bool bossSpawned = false;
    bool isDead = false;

    void Start()
    {
        hp = maxHp;
       
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("보스가 사망했습니다."); 

        // 보스가 죽으면 StageManager에게 알림
        StageManager.Instance.BossDefeated(); 

        // 보스 오브젝트 삭제
        Destroy(gameObject);
    }
    public void Attack() // 보스가 플레이어 공격 ,타격
    {
        int i = 0;
        //GameManager.Instance.friendlyCharacterList[i].stat.startMaxHealth -= atk;
    }
    public void TakeDamage(int damage) // 피격
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();

        }

    }
}
