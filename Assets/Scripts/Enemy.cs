using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private Transform Player;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float maxDistanceChase = 25f;
    [SerializeField] private float minDistanceChase = 10f;
    private bool isDeath = false;


    private void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= minDistanceChase)
        {
            transform.LookAt(Player.transform);
            transform.position = Vector3.MoveTowards(transform.position, 
                Player.transform.position, moveSpeed *  Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, Player.transform.position) >= maxDistanceChase)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        maxHealth -= damageAmount;
        if (maxHealth <= 0)
        {
            isDeath = true;
            Debug.Log("Враг умер");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Нанесён урон");
        }
    }
}
