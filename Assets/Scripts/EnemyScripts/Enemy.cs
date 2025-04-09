using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP = 20;
    
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
    }
}
