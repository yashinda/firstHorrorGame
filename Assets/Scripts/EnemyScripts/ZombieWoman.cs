using UnityEngine;

public class ZombieWoman : Enemy
{
    private bool isHit = false;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        Idle();
        Walking();
        Attack();
    }

    public void TakeHit()
    {
        if (HP <= 0)
        {
            animator.SetTrigger("Death");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Hit");
            isHit = true;
        }
    }

    private void Idle()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("ZombieHit") && stateInfo.normalizedTime >= 1.0f)
        {
            isHit = false;
        }

        if (!isChasing && !isAttacking && (!isDeath || !isHit))
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }
    }

    private void Walking()
    {
        if (isChasing && !isAttacking)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void Attack()
    {
        if (isAttacking)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
}
