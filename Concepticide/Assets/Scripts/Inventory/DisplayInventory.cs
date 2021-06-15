using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public bool isPlayerInventory = true;
    public MouseItem mouseItem = new MouseItem();

    public GameObject inventoryPrefab;
    public InventoryObject inventory;

    public int xStart;
    public int yStart;
    public int xSpaceItems;
    public int ySpaceItems;
    public int colNb;

    private Dictionary<GameObject, InventorySlot> itemsDisplay = new Dictionary<GameObject, InventorySlot>();

    private void Start() {
        CreateSlots();
    }

    // TODO: Change to an event
    private void Update() => UpdateSlots();

    // Inventory at start
    private void CreateSlots() {
        itemsDisplay = new Dictionary<GameObject, InventorySlot>();

        for (var i = 0; i < inventory.container.items.Length; i++) {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            if (isPlayerInventory) {
                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragStop(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            }

            itemsDisplay.Add(obj, inventory.container.items[i]);
        }
    }

    private void UpdateSlots() {
        foreach (var slot in itemsDisplay) {
            // There is an item
            if (slot.Value.id >= 0) {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[slot.Value.id].uiDisplay;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = slot.Value.amount == 1 ? "" : slot.Value.amount.ToString("n0");
            }
            // No items ==> clear the slot
            else {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void AddEvent(GameObject obj, EventTriggerType eType, UnityAction<BaseEventData> action) {
        var trigger = obj.GetComponent<EventTrigger>();

        var eventTrigger = new EventTrigger.Entry {eventID = eType};
        eventTrigger.callback.AddListener(action);

        trigger.triggers.Add(eventTrigger);
    }

    // =====================
    // ACTIONS
    // =====================

    public void OnEnter(GameObject obj) {
        mouseItem.hoverObj = obj;
        if (itemsDisplay.ContainsKey(obj))
            mouseItem.hoverSlot = itemsDisplay[obj];
    }

    public void OnExit(GameObject obj) {
        mouseItem.hoverObj = null;
        mouseItem.hoverSlot = null;
    }

    public void OnDragStart(GameObject obj) {
        var mouseObj = new GameObject();

        var rt = mouseObj.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(80, 80);
        mouseObj.transform.SetParent(transform.parent);

        if (itemsDisplay[obj].id < 0)
            return;

        var img = mouseObj.AddComponent<Image>();
        img.sprite = inventory.database.getItem[itemsDisplay[obj].id].uiDisplay;
        img.raycastTarget = false;

        mouseItem.obj = mouseObj;
        mouseItem.item = itemsDisplay[obj];
    }

    public void OnDragStop(GameObject obj) {
        if (mouseItem.hoverObj) {
            inventory.MoveItems(itemsDisplay[obj], itemsDisplay[mouseItem.hoverObj]);
        }
        else {
            inventory.RemoveItem(itemsDisplay[obj].item);
        }

        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }

    public void OnDrag(GameObject obj) {
        if (mouseItem.obj != null) {
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    // Calculates the position in the inventory panel
    private Vector3 GetPosition(int i) {
        return new Vector3(xStart + xSpaceItems * (i % colNb), yStart - ySpaceItems * (i / colNb), 0f);
    }
}

public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverSlot;
    public GameObject hoverObj;
}