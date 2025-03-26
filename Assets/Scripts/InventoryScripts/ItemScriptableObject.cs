using UnityEngine;

public enum ItemType {Default, Food, Ammo, Drink}
public class ItemScriptableObject : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public int maximumAmount;
    public Sprite icon;
    public string itemDescription;
    public GameObject itemPrefab;
}
