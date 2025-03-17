using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private Transform Player;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float minDistanceChase = 10f;
    [SerializeField] private NavMeshAgent enemyAgent;
    private bool isDeath = false;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if (distanceToPlayer <= minDistanceChase)
        {
            enemyAgent.SetDestination(Player.transform.position);
        }
        else
        {
            enemyAgent.ResetPath();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        maxHealth -= damageAmount;
        if (maxHealth <= 0)
        {
            isDeath = true;
            Destroy(gameObject);
        }
    }
}
