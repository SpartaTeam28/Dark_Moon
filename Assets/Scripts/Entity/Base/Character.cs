using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStat stat;
    public CharacterTraitManager traitManager;
    public CharacterInfo info;
    public AnimationHandler animationHandler;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Sprite icon;
    public AnimatorOverrideController animatorController;

    public GameObject turn;

    private void Awake()
    {

        stat = GetComponent<CharacterStat>();
        traitManager = GetComponent<CharacterTraitManager>();
        info = GetComponent<CharacterInfo>();
        animationHandler = GetComponent<AnimationHandler>();

        stat.character = this;
        info.character = this;
    }

    public void TakeDamaged(float amount, float target_attack, float target_critical, float target_accuracy)
    {
        var health = stat.health;
        if(health == null)
        {
            Debug.LogError("Character dose not have Health Component.");
            return;
        }
        var evasion = Random.Range(0, 100);
        var accuary = Random.Range(0, 100);
        var critical = Random.Range(0, 100);
        if (evasion < stat.evasion.value)
        {
            Debug.Log($"{info.name}이(가) 회피 했습니다.");
            return;
        }
        if (accuary < target_accuracy)
        {
            Debug.Log($"공격이 빗나갔습니다.");
            return;
        }
        if (critical < target_critical)
        {
            amount *= 1.5f;
        }

        amount = (100 / (100 + stat.defence.value)) * (amount + stat.attack.value);
        health.AddHealth(amount * -1);
        if(health.curHealth <= 0)
        {
            OnDie();
            return;
        }
    }

    private void OnDie()
    {
        Battle_Silhum.Instance.PlayerDie(this);
    }

    public void StartTurn()
    {
        turn.SetActive(true);
    }

    public void EndTurn()
    {
        turn.SetActive(false);
    }
}
