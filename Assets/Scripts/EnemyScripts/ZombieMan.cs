using UnityEngine;

public class ZombieMan : Enemy
{
    private bool isDeath = false;

    public void TakeHit()
    {
        if (HP <= 0)
        {
            isDeath = true;
            Debug.Log("Zombie is Death");
        }
    }

}
