using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="Food Item", menuName = "Inventory/Items/New Food Item")]

public class FoodItem : ItemScriptableObject
{
    public float healAmount;

    void Start()
    {
        itemType = ItemType.Food;
    }
}
