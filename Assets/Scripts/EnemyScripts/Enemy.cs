using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public int HP = 20;
    public bool isDeath = false;

    public Transform player;
    public float moveSpeed = 2.0f;

    private float distanceToPlayer;
    public float minDistanceChase = 25.0f;
    public NavMeshAgent agent;

    public new Rigidbody rigidbody;

    protected private bool isChasing = false;
    protected private bool isAttacking = false;

    protected virtual void Update()
    {
        if (!isDeath)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            ChasePlayer();
        }
        else
        {
            isChasing = false;
            agent.ResetPath();
        }
    }

    private void ChasePlayer()
    {
        if (distanceToPlayer <= minDistanceChase)
        {
            agent.SetDestination(player.position);
            isChasing = true;
        }
        else
        {
            agent.ResetPath();
            isChasing = false;
        }

        if (distanceToPlayer <= 2.0f)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (gameObject.GetComponent<ZombieWoman>() != null)
        {
            gameObject.GetComponent<ZombieWoman>().TakeHit();
        }
        
        if (gameObject.GetComponent<ZombieMan>() != null)
        {
            gameObject.GetComponent<ZombieMan>().TakeHit();
        }

        if (HP <= 0)
        {
            isDeath = true;
        }   
    }
}
