using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<JobData> jobs;
    public List<Character> enemyLists;

    public int minLevel;
    public int maxLevel;

    private void Awake()
    {
        CreateEnemy();
    }
    public void CreateEnemy()
    {
        for (int i = 0; i < enemyLists.Count; i++)
        {
            enemyLists[i].info.job = jobs[Random.Range(0, jobs.Count)];
            enemyLists[i].info.level = Random.Range(minLevel, maxLevel);
            GameManager.instance.EnemyCharacterList.Add(enemyLists[i]);
        }
    }
}
