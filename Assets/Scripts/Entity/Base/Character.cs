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

    private void Awake()
    {

        stat = GetComponent<CharacterStat>();
        traitManager = GetComponent<CharacterTraitManager>();
        info = GetComponent<CharacterInfo>();
        animationHandler = GetComponent<AnimationHandler>();

        stat.character = this;
        info.character = this;
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
        Battle_Silhum.Instance.PlayerDie(this);
    }
}
