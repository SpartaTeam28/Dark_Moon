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
    }
    private void Start()
    {
        character = GetComponent<Character>();
        character.spriteRenderer.sprite = job.sprite;
        character.icon = job.icon;
        character.animatorController = job.animatorController;
        character.animator.runtimeAnimatorController = character.animatorController;
        SetLevel();
        if (characterName == "")
        {
            characterName = GameManager.Instance.nameGenerator.GetRandomName(job.isMale);
        }
    }

    public void SetLevel()
    {
        for (int i = 1; i < level; i++) 
        {
            character.stat.LevelUp();
        }
    }

    public void AddExp(int exp)
    {
        curExp += exp;
        while (curExp >= totalExp)
        {
            LevelUp();
            curExp = curExp - totalExp; // ·¹º§ ¾÷ ÈÄ ÇöÀç exp¿¡¼­ ÃÑexp¸¦ »­
            totalExp += addtotalExp; // ÃÑexp¸¦ 15¾¿ ´Ã¸®°ÔÇÔ
        }
    }

    public void LevelUp()
    {
        level++;
        character.stat.LevelUp();
    }
}
