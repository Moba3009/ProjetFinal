using TMPro;
using UnityEngine;

public class AnimationLinker : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }



    public void ResetAttack()
    {
        _animator.SetBool("IsAttacking", false);
    }

    public void Death()
    {
        _animator.SetBool("IsDeath", true);
    }

    public void Idle()
    {
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsAttacking", false);
    }

    public void Attack()
    {
        _animator.SetBool("IsAttacking", true);

    }

    public void Walk()
    {
        _animator.SetBool("IsWalking", true);

    }

    public void Stop()
    {
        _animator.SetBool("IsWalking", false);

    }

}