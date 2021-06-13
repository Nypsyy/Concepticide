using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;

    public int xStart;
    public int yStart;
    public int xSpaceItems;
    public int ySpaceItems;
    public int colNb;


    private Dictionary<InventorySlot, GameObject> itemsDisplay = new Dictionary<InventorySlot, GameObject>();

    private GameObject CreateNewItem(InventorySlot slot) {
        // Instantiating object with inventory panel as parent
        var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
        obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[slot.id].uiDisplay;
        // Amount
        obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
        // Add to the display dictionary
        itemsDisplay.Add(slot, obj);

        return obj;
    }

    private void Start() {
        CreateDisplay();
    }

    // Calculates the position in the inventory panel
    private Vector3 GetPosition(int i) {
        return new Vector3(xStart + xSpaceItems * (i % colNb), yStart - ySpaceItems * (i / colNb), 0f);
    }

    // Inventory at start
    private void CreateDisplay() {
        for (var i = 0; i < inventory.container.items.Count; i++) {
            var obj = CreateNewItem(inventory.container.items[i]);
            // Position in the panel
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        }
    }

    private void Update() => UpdateDisplay();

    // When the inventory changes
    private void UpdateDisplay() {
        for (var i = 0; i < inventory.container.items.Count; i++) {
            var slot = inventory.container.items[i];

            // If already in the inventory
            if (itemsDisplay.ContainsKey(slot)) {
                // Updating amount display
                itemsDisplay[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            }
            else {
                var obj = CreateNewItem(slot);
                // Position in the panel
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
        }
    }
}