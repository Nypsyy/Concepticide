using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory", order = 0)]
public class InventoryObject : ScriptableObject
{
    // List of all objects held in the inventory
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(ItemObject item, int amount) {
        var hasItem = false;

        // Adds the amount if the item already exists
        foreach (var slot in container.Where(slot => slot.item == item)) {
            slot.Add(amount);
            hasItem = true;
            break;
        }

        // Else creates and adds the item in the inventory
        if (!hasItem) {
            container.Add(new InventorySlot(item, amount));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item; // The scriptable object
    public int amount; // How much

    // Basic constructor
    public InventorySlot(ItemObject item, int amount) {
        this.item = item;
        this.amount = amount;
    }
    
    public void Add(int val) {
        amount += val;
    }
}