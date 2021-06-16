using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;

    protected override void CreateSlots() {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        for (var i = 0; i < inventory.container.items.Length; i++) {
            var obj = slots[i];

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
}