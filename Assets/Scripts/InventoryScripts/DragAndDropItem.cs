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
            int amountToDrop = isSplittingStack ? Mathf.FloorToInt(oldSlot.amount / 2f) : oldSlot.amount;
            GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward, 
                Quaternion.identity);
            itemObject.GetComponent<Item>().amount = oldSlot.amount;

            if (isSplittingStack)
            {
                oldSlot.amount -= amountToDrop;
                oldSlot.itemAmountText.text = oldSlot.amount.ToString();
                if (oldSlot.amount <= 0)
                {
                    NullifySlotData();
                }
            }
            else
            {
                NullifySlotData();
            }
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            InventorySlot newSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>();

            if (isSplittingStack && oldSlot.amount > 1)
            {
                SplitStackToSlot(newSlot);
            }
            else
            {
                ExchangeSlotData(newSlot);
            }
        }
    }

    private void SplitStackToSlot(InventorySlot newSlot)
    {
        int halfAmount = Mathf.FloorToInt(oldSlot.amount / 2f);

        if (newSlot.isEmpty)
        {
            newSlot.item = oldSlot.item;
            newSlot.amount = halfAmount;
            newSlot.isEmpty = false;
            newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
            newSlot.itemAmountText.text = halfAmount.ToString();

            oldSlot.amount -= halfAmount;
            oldSlot.itemAmountText.text = oldSlot.amount.ToString();
        }
        else if (newSlot.item = oldSlot.item)
        {
            newSlot.amount += halfAmount;
            newSlot.itemAmountText.text = newSlot.amount.ToString();

            oldSlot.amount -= halfAmount;
            oldSlot.itemAmountText.text = oldSlot.amount.ToString();

            if (oldSlot.amount <= 0)
            {
                NullifySlotData();
            }
        }
        else
        {
            ExchangeSlotData(newSlot);
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