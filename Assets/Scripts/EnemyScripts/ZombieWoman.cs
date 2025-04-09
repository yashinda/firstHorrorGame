using UnityEngine;

public class ZombieWoman : Enemy
{
    private bool isHit = false;
    public Animator animator;
    private Vector3 velocity;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        Idle();
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

        if (!isChasing && (!isDeath || !isHit))
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }
    }
}
