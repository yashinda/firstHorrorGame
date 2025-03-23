using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLife = 3.0f;
    [SerializeField] private int damageAmount = 5;

    private void Awake()
    {
        Destroy(gameObject, bulletLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }
}
