using UnityEngine;

public class ZombieMan : Enemy
{
    public void TakeHit()
    {
        if (HP <= 0)
        {
            isDeath = true;
            Destroy(gameObject);
        }
    }

}
