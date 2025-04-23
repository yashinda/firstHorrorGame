using UnityEngine;

public class Ammo : Interactable
{
    public int ammoAmount = 20;
    public GameObject weapon;

    protected override void Interact()
    {
        Debug.Log(textMessage);
        weapon.GetComponent<Weapon>().AddAmmo(ammoAmount);
        Destroy(gameObject);
    }
}
