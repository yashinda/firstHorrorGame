using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP = 20;
    private bool isHit = false;
    private bool isDeath = false;
    public Animator animator;
    private Vector3 velocity;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Idle();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            animator.SetTrigger("Death");
            GetComponent<Collider>().enabled = false;
            isDeath = true;
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


        if (velocity.y == 0 && velocity.x == 0 && velocity.z == 0 && (!isDeath || !isHit))
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }
    }
}
