using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public int xStart;
    public int yStart;
    public int xSpaceItems;
    public int ySpaceItems;
    public int colNb;


    private Dictionary<InventorySlot, GameObject> itemsDisplay = new Dictionary<InventorySlot, GameObject>();

    private GameObject CreateNewItem(InventorySlot slot) {
        // Instantiating object with inventory panel as parent
        var obj = Instantiate(slot.item.prefab, Vector3.zero, Quaternion.identity, transform);
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
        for (var i = 0; i < inventory.container.Count; i++) {
            var obj = CreateNewItem(inventory.container[i]);
            // Position in the panel
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        }
    }

    private void Update() {
        UpdateDisplay();
    }

    // When the inventory changes
    private void UpdateDisplay() {
        for (var i = 0; i < inventory.container.Count; i++) {
            // If already in the inventory
            if (itemsDisplay.ContainsKey(inventory.container[i])) {
                // Updating amount display
                itemsDisplay[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    inventory.container[i].amount.ToString("n0");
            }
            else {
                var obj = CreateNewItem(inventory.container[i]);
                // Position in the panel
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
        }
    }
}