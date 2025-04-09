using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public int HP = 20;
    public bool isDeath = false;

    public Transform player;
    public float moveSpeed = 3.0f;

    public float minDistanceChase = 25.0f;
    public NavMeshAgent agent;

    public new Rigidbody rigidbody;

    protected private bool isChasing = false;

    protected virtual void Update()
    {
        if (!isDeath)
        {
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
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

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
