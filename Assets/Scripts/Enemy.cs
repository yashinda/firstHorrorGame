using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 20f;
    private bool isDeath = false;
    
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
}
