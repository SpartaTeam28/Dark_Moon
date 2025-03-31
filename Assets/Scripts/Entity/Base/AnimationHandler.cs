using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private readonly int IsAttack = Animator.StringToHash("IsAttack");

    public Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger(IsAttack);
    }
}
