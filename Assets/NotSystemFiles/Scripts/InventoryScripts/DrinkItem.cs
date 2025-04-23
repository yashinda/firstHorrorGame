using UnityEngine;

[CreateAssetMenu(fileName = "Drink Item", menuName = "Inventory/Items/New Drink Item")]

public class DrinkItem : ItemScriptableObject
{
    public float staminaAmount;

    void Start()
    {
        itemType = ItemType.Drink;
    }
}
