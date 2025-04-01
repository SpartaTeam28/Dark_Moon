using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public Character character;
    public JobData job;

    public string characterName;
    public int level = 1;
    public int totalExp = 15;
    public int curExp = 0;
    private int addtotalExp = 15;
    
    private void Awake()
    {
        character = GetComponent<Character>();

        character.spriteRenderer.sprite = job.sprite;
        character.icon = job.icon;
        character.animatorController = job.animatorController;
        character.animator.runtimeAnimatorController = character.animatorController;
    }

    private void Start()
    {
        SetLevel();
        if (characterName == "")
        {
            characterName = GameManager.Instance.nameGenerator.GetRandomName(job.isMale);
        }
    }

    private void SetLevel()
    {
        for (int i = 1; i < level; i++) 
        {
            LevelUp();
        }
    }

    public void AddExp(int exp)
    {
        curExp += exp;
        while (curExp >= totalExp)
        {
            LevelUp();
            curExp = curExp - totalExp;
            totalExp += addtotalExp;
        }
    }

    public void LevelUp()
    {
        level++;
        character.stat.LevelUp();
    }
}
