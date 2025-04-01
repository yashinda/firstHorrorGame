using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InventorySlot oldSlot;
    private Transform player;
    private bool isSplittingStack;
    private bool isTakeOne;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        oldSlot = transform.GetComponentInParent<InventorySlot>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;

        isSplittingStack = Input.GetKey(KeyCode.LeftShift);
        isTakeOne = Input.GetKey(KeyCode.LeftControl);

        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        GetComponentInChildren<Image>().raycastTarget = false;
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;

        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        GetComponentInChildren<Image>().raycastTarget = true;
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;

        if (eventData.pointerCurrentRaycast.gameObject.name == "UIPanelInventory")
        {
            HandleDropToWorld();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            HandleDropToSlot(eventData);
        }
    }

    private void HandleDropToWorld()
    {
        int amountToDrop = GetAmountToTransfer();
        GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward,
            Quaternion.identity);
        itemObject.GetComponent<Item>().amount = amountToDrop;

        oldSlot.amount -= amountToDrop;
        oldSlot.itemAmountText.text = oldSlot.amount.ToString();

        if (oldSlot.amount <= 0)
        {
            NullifySlotData();
        }
    }
    private void HandleDropToSlot(PointerEventData eventData)
    {
        InventorySlot newSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>();

        if (isTakeOne)
        {
            if (CanTransferItems(newSlot, 1))
            {
                TakeOneFromSlot(newSlot);
            }
            else
            {
                ExchangeSlotData(newSlot);
            }
        }
        else if (isSplittingStack)
        {
            int halfAmount = Mathf.FloorToInt(oldSlot.amount / 2f);
            if (CanTransferItems(newSlot, halfAmount))
            {
                SplitStackToSlot(newSlot);
            }
            else
            {
                ExchangeSlotData(newSlot);
            }
        }
        else
        {
            ExchangeSlotData(newSlot);
        }
    }

    private bool CanTransferItems(InventorySlot targetSlot, int amount)
    {
        if (targetSlot.isEmpty)
            return true;
        if (targetSlot.item != oldSlot.item)
            return false;

        return (targetSlot.amount + amount) <= targetSlot.item.maximumAmount;
    }

    private int GetAmountToTransfer()
    {
        if (isTakeOne) return Mathf.Min(1, oldSlot.amount);
        if (isSplittingStack) return Mathf.FloorToInt(oldSlot.amount / 2f);
        return oldSlot.amount;
    }

    private void SplitStackToSlot(InventorySlot newSlot)
    {
        int amountToTransfer = Mathf.FloorToInt(oldSlot.amount / 2f);
        TransferItemsToSlot(newSlot, amountToTransfer);
    }

    private void TakeOneFromSlot(InventorySlot newSlot)
    {
        TransferItemsToSlot(newSlot, 1);
    }

    private void TransferItemsToSlot(InventorySlot newSlot, int amount)
    {
        amount = Mathf.Min(amount, oldSlot.amount);

        if (newSlot.isEmpty)
        {
            newSlot.item = oldSlot.item;
            newSlot.amount = amount;
            newSlot.isEmpty = false;
            newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
            newSlot.itemAmountText.text = amount.ToString();
            oldSlot.amount -= amount;
            oldSlot.itemAmountText.text = oldSlot.amount.ToString();
        }
        else if (newSlot.item == oldSlot.item)
        {
            newSlot.amount += amount;
            newSlot.itemAmountText.text = newSlot.amount.ToString();
        }
        else
        {
            ExchangeSlotData(newSlot);
            return;
        }

        oldSlot.amount -= amount;
        oldSlot.itemAmountText.text = oldSlot.amount.ToString();

        if (oldSlot.amount <= 0)
        {
            NullifySlotData();
        }
    }

    public void NullifySlotData()
    {
        oldSlot.item = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot.iconGO.GetComponent<Image>().sprite = null;
        oldSlot.itemAmountText.text = "";
    }

    void ExchangeSlotData(InventorySlot newSlot)
    {
        ItemScriptableObject tempItem = oldSlot.item;
        int tempAmount = oldSlot.amount;
        bool tempIsEmpty = oldSlot.isEmpty;
        Sprite tempSprite = oldSlot.iconGO.GetComponent<Image>().sprite;

        oldSlot.item = newSlot.item;
        oldSlot.amount = newSlot.amount;
        oldSlot.isEmpty = newSlot.isEmpty;

        if (!newSlot.isEmpty)
        {
            oldSlot.iconGO.GetComponent<Image>().sprite = newSlot.iconGO.GetComponent<Image>().sprite;
            oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            oldSlot.itemAmountText.text = newSlot.amount.ToString();
        }
        else
        {
            oldSlot.iconGO.GetComponent<Image>().sprite = null;
            oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.itemAmountText.text = "";
        }

        newSlot.item = tempItem;
        newSlot.amount = tempAmount;
        newSlot.isEmpty = tempIsEmpty;

        if (!tempIsEmpty)
        {
            newSlot.iconGO.GetComponent<Image>().sprite = tempSprite;
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            newSlot.itemAmountText.text = tempAmount.ToString();
        }
        else
        {
            newSlot.iconGO.GetComponent<Image>().sprite = null;
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.itemAmountText.text = "";
        }
    }
}