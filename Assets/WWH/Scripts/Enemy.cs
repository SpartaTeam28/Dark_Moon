
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{

    public SKilldata[] sKilldatas;


    private void OnMouseOver()
    {
        if(ClickManager.Instance.isAttacking)
        {
            if(!ClickManager.Instance.GetSkilldata().isBuff && !ClickManager.Instance.GetSkilldata().isHeal)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
        if (ClickManager.Instance.isAttacking)
        {
            if (!ClickManager.Instance.GetSkilldata().isBuff && !ClickManager.Instance.GetSkilldata().isHeal)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void SkillActive(SKilldata sKilldata)
    {
        Character[] Enemycharacters = GameManager.instance.EnemyCharacterList.ToArray();
        Character[] Playercharacter = GameManager.instance.friendlyCharacterList.ToArray();

        Character[] ActiveEnemyList = Enemycharacters.Where(Ob => Ob.gameObject.activeSelf).ToArray();
        Character[] ActivePlayerList = Playercharacter.Where(OB => OB.gameObject.activeSelf).ToArray();


        if(sKilldata.skillTargetCount == 1)
        {

            Character RandomEnemy = ActiveEnemyList[Random.Range(0, ActiveEnemyList.Length)];
            Character RandomPlayer = ActivePlayerList[Random.Range(0, ActivePlayerList.Length)];
            

            if (sKilldata.isHeal)
            {
                transform.GetComponent<AnimationHandler>().Attack();
                RandomEnemy.transform.GetChild(0).gameObject.SetActive(true) ;
                ClickManager.Instance.Heal(RandomEnemy.stat, sKilldata);

            }
            else if (sKilldata.isBuff)
            {
                transform.GetComponent<AnimationHandler>().Attack();
                RandomEnemy.transform.GetChild(0).gameObject.SetActive(true);
                ClickManager.Instance.Buff(RandomEnemy.GetComponent<CharacterStat>(), sKilldata);
            }
            else if (sKilldata.isDebuff)
            {
                transform.GetComponent<AnimationHandler>().Attack();
                RandomPlayer.transform.GetChild(0).gameObject.SetActive(true);
                ClickManager.Instance.Debuff(RandomPlayer.GetComponent<CharacterStat>(), sKilldata);
            }
            else
            {
                transform.GetComponent<AnimationHandler>().Attack();
                ClickManager.Instance.SetSkilldata(sKilldata);
                RandomPlayer.transform.GetChild(0).gameObject.SetActive(true);
                RandomPlayer.TakeDamaged(sKilldata.skillDamage);
            }
        }
        else
        {

            if(sKilldata.isHeal)
            {
                transform.GetComponent<AnimationHandler>().Attack();
                foreach (Character stat in ActiveEnemyList) 
                {
                    ClickManager.Instance.Heal(stat.stat, sKilldata);
                    stat.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else if(sKilldata.isBuff)
            {
                transform.GetComponent<AnimationHandler>().Attack();
                foreach (Character stat in ActiveEnemyList)
                {
                    ClickManager.Instance.Buff(stat.GetComponent<CharacterStat>(), sKilldata);
                    stat.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else if( sKilldata.isDebuff)
            {
                transform.GetComponent<AnimationHandler>().Attack();
                foreach (Character stat in ActivePlayerList)
                {
                    ClickManager.Instance.Debuff(stat.GetComponent<CharacterStat>(), sKilldata);
                    stat.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else
            {
                transform.GetComponent<AnimationHandler>().Attack();
                ClickManager.Instance.SetSkilldata(sKilldata);
                foreach (Character stat in ActivePlayerList)
                {
                    stat.TakeDamaged(sKilldata.skillDamage);
                    stat.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
        
    }

}
