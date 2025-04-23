using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 2.0f;
    public int damageAmount = 10;

    private void Awake()
    {
        Destroy(gameObject, bulletLife);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);

        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
    }
}
