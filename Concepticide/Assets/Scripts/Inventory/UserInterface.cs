using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UserInterface : MonoBehaviour
{
    public bool isPlayerInventory = true;

    public GameObject slotPrefab;
    public InventoryObject inventory;

    protected Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    private void Start() {
        foreach (var slot in inventory.GetSlots) {
            slot.uiParent = this;
            slot.onAfterUpdate += OnSlotUpdate;
        }

        CreateSlots();

        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    // private void Update() => slotsOnInterface.UpdateSlotDisplay();

    private void OnSlotUpdate(InventorySlot slot) {
        // There is an item
        if (slot.item.id >= 0) {
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.ItemObject.uiDisplay;
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount == 1 ? "" : slot.amount.ToString("n0");
        }
        // No items ==> clear the slot
        else {
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    // Inventory at start
    protected abstract void CreateSlots();

    private void UpdateSlots() {
        foreach (var slot in slotsOnInterface) {
            // There is an item
            if (slot.Value.item.id >= 0) {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.Value.ItemObject.uiDisplay;
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

    protected void AddEvent(GameObject obj, EventTriggerType eType, UnityAction<BaseEventData> action) {
        var trigger = obj.GetComponent<EventTrigger>();

        var eventTrigger = new EventTrigger.Entry {eventID = eType};
        eventTrigger.callback.AddListener(action);

        trigger.triggers.Add(eventTrigger);
    }

    public GameObject CreateTmpItem(GameObject obj) {
        if (slotsOnInterface[obj].item.id < 0) return null;

        var tmpItem = new GameObject();

        var rt = tmpItem.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(80, 80);
        tmpItem.transform.SetParent(transform.parent);

        var img = tmpItem.AddComponent<Image>();
        img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
        img.raycastTarget = false;

        return tmpItem;
    }

    // =====================
    // ACTIONS
    // =====================

    public void OnEnter(GameObject obj) {
        MouseData.slotHovered = obj;
    }

    public void OnExit(GameObject obj) {
        MouseData.slotHovered = null;
    }

    public void OnDragStart(GameObject obj) {
        MouseData.tmpItemDragged = CreateTmpItem(obj);
    }

    public void OnDragStop(GameObject obj) {
        Destroy(MouseData.tmpItemDragged);

        if (MouseData.interfaceMouseIsOver == null) {
            slotsOnInterface[obj].RemoveItem();
            return;
        }

        if (!MouseData.slotHovered) return;

        var mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHovered];
        inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
    }

    public void OnDrag(GameObject obj) {
        if (MouseData.tmpItemDragged != null) {
            MouseData.tmpItemDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void OnEnterInterface(GameObject obj) {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }

    public void OnExitInterface(GameObject obj) {
        MouseData.interfaceMouseIsOver = null;
    }
}

public static class MouseData
{
    public static GameObject tmpItemDragged;
    public static GameObject slotHovered;
    public static UserInterface interfaceMouseIsOver;
}