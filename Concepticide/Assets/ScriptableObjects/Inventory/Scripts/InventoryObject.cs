using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/**
 * =====================================
 * INVENTORY OBJECT CLASS
 * =====================================
 */
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory", order = 0)]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    public string savePath;
    public Inventory container;
    public InventorySlot[] GetSlots => container.slots;

    private int EmptySlotCount {
        get { return GetSlots.Count(slot => slot.item.id <= -1); }
    }

    public void SwapItems(InventorySlot slot1, InventorySlot slot2) {
        if (!slot2.CanPlaceInSlot(slot1.ItemObject) || !slot1.CanPlaceInSlot(slot2.ItemObject))
            return;

        var tmp = new InventorySlot(slot2.item, slot2.amount);
        slot2.UpdateSlot(slot1.item, slot1.amount);
        slot1.UpdateSlot(tmp.item, tmp.amount);
    }

    private InventorySlot SetFirstEmptySlot(Item item, int amount) {
        foreach (var slot in GetSlots) {
            if (slot.item.id > -1) continue;

            slot.UpdateSlot(item, amount);
            return slot;
        }

        // TODO: When inventory is full
        return null;
    }

    public bool AddItem(Item item, int amount) {
        if (EmptySlotCount <= 0)
            return false;

        var slot = FindItemInInventory(item);

        if (slot == null || !database.itemObjects[item.id].stackable) {
            SetFirstEmptySlot(item, amount);
            return true;
        }

        slot.AddAmount(amount);
        return true;
    }

    public InventorySlot FindItemInInventory(Item item) {
        return GetSlots.FirstOrDefault(slot => slot.item.id == item.id);
    }

    [ContextMenu("Save")]
    public void Save() {
        Debug.Log("Saving inventory...");
        // SAVE DATA to JSON format, allowing player personalisation
        /*
        // Json format
        var saveData = JsonUtility.ToJson(this, true);
        // Create a saving file to a common path no matter the platform
        var bf = new BinaryFormatter();
        var fs = File.Create(string.Concat(Application.persistentDataPath, savePath));

        // Serializing and closing
        bf.Serialize(fs, saveData);
        fs.Close();
        */


        // SAVE DATA with a formatter, easier but no edition possible
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, container);
        stream.Close();
        Debug.Log("Inventory saved");
    }

    [ContextMenu("Load")]
    public void Load() {
        Debug.Log("Loading inventory...");
        // Checking the file
        if (!File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            return;

        // LOAD DATA JSON format
        /*
        // Opening
        var bf = new BinaryFormatter();
        var fs = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);

        // Deserializing and formatting for object
        JsonUtility.FromJsonOverwrite(bf.Deserialize(fs).ToString(), this);
        fs.Close();
        */

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
        var newContainer = (Inventory) formatter.Deserialize(stream);

        for (var i = 0; i < newContainer.slots.Length; i++) {
            GetSlots[i].UpdateSlot(newContainer.slots[i].item, newContainer.slots[i].amount);
        }

        stream.Close();
        Debug.Log("Inventory loaded");
    }

    [ContextMenu("Clear")]
    public void Clear() {
        container.Clear();
    }
}

/**
 * =====================================
 * INVENTORY CLASS
 * =====================================
 */
[System.Serializable]
public class Inventory
{
    // List of all objects held in the inventory
    public InventorySlot[] slots = new InventorySlot[GameUtils.InventorySize];

    public void Clear() {
        foreach (var slot in slots)
            slot.RemoveItem();
    }
}

public delegate void SlotUpdated(InventorySlot slot);

/**
 * =====================================
 * INVENTORY SLOT CLASS
 * =====================================
 */
[System.Serializable]
public class InventorySlot
{
    [System.NonSerialized]
    public UserInterface uiParent;

    [System.NonSerialized]
    public GameObject slotDisplay;

    [System.NonSerialized]
    public SlotUpdated onBeforeUpdate;

    [System.NonSerialized]
    public SlotUpdated onAfterUpdate;


    public ItemType[] allowedItems = new ItemType[0];
    public Item item;  // The scriptable object
    public int amount; // How much

    public ItemObject ItemObject => item.id >= 0 ? uiParent.inventory.database.itemObjects[item.id] : null;

    // Basic constructor
    public InventorySlot() {
        UpdateSlot(new Item(), 0);
    }

    public InventorySlot(Item item, int amount) {
        UpdateSlot(item, amount);
    }

    public void UpdateSlot(Item item, int amount) {
        onBeforeUpdate?.Invoke(this);

        this.item = item;
        this.amount = amount;

        onAfterUpdate?.Invoke(this);
    }

    public bool CanPlaceInSlot(ItemObject itemObj) {
        return allowedItems.Length <= 0 || itemObj == null || itemObj.itemData.id < 0 || allowedItems.Any(type => itemObj.type == type);
    }

    public void RemoveItem() {
        UpdateSlot(new Item(), 0);
    }

    public void AddAmount(int val) {
        amount += val;
    }
}