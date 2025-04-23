using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuickSlotInventory : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public Transform quickslotParent;
    public InventoryManager inventoryManager;
    public int currentQuickslotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public TMP_Text healthText;
    public TMP_Text staminaText;

    private void Start()
    {
        healthText.text = playerHealth.currentHealth.ToString();
        staminaText.text = Mathf.RoundToInt(playerMovement.currentStamina).ToString();
    }

    void Update()
    {
        healthText.text = playerHealth.currentHealth.ToString();
        staminaText.text = Mathf.RoundToInt(playerMovement.currentStamina).ToString();

        for (int i = 0; i < quickslotParent.childCount; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString())) {
                if (currentQuickslotID == i)
                {
                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    }
                }
                else
                {
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    currentQuickslotID = i;
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item != null)
            {
                if (quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item.isConsumeable && !inventoryManager.isOpen && quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == selectedSprite)
                {
                    ChangeCharacteristics();

                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount <= 1)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount--;
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().itemAmountText.text = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount.ToString();
                    }
                }
            }
        }
    }

    private void ChangeCharacteristics()
    {
        int healthChange = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item.changeHealth;
        int staminaChange = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item.changeStamina;

        if (int.Parse(healthText.text) + healthChange <= 100)
        {
            playerHealth.currentHealth += healthChange;
        }
        else
        {
            playerHealth.currentHealth = 100;
            healthText.text = "100";
        }

        if (int.Parse(staminaText.text) + staminaChange <= 100)
        {
            playerMovement.currentStamina += staminaChange;
        }
        else
        {
            playerMovement.currentStamina = 100;
            staminaText.text = "100";
        }
    }
}
