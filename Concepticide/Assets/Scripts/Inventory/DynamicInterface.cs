using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public int xStart;
    public int yStart;
    public int xSpaceItems;
    public int ySpaceItems;
    public int colNb;

    protected override void CreateSlots() {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        for (var i = 0; i < inventory.container.items.Length; i++) {
            var obj = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            if (isPlayerInventory) {
                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragStop(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            }

            slotsOnInterface.Add(obj, inventory.container.items[i]);
        }
    }

    // Calculates the position in the inventory panel
    private Vector3 GetPosition(int i) {
        return new Vector3(xStart + xSpaceItems * (i % colNb), yStart - ySpaceItems * (i / colNb), 0f);
    }
}