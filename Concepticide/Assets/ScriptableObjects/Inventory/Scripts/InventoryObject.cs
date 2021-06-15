using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory", order = 0)]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    public string savePath;

    public Inventory container;

    public void RemoveItem(Item item) {
        for (var i = 0; i < container.items.Length; i++) {
            if (container.items[i].item == item) {
                container.items[i].RemoveSlot();
            }
        }
    }

    public void MoveItems(InventorySlot slot1, InventorySlot slot2) {
        var tmp = new InventorySlot(slot2.id, slot2.item, slot2.amount);
        slot2.UpdateSlot(slot1.id, slot1.item, slot1.amount);
        slot1.UpdateSlot(tmp.id, tmp.item, tmp.amount);
    }

    public InventorySlot SetFirstEmptySlot(Item item, int amount) {
        foreach (var slot in container.items) {
            if (slot.id > -1) continue;

            slot.UpdateSlot(item.id, item, amount);
            return slot;
        }

        // TODO: When inventory is full
        return null;
    }

    public void AddItem(Item item, int amount) {
        // Adds the amount if the item already exists
        foreach (var slot in container.items.Where(slot => slot.id == item.id)) {
            slot.Add(amount);
            return;
        }

        // Else creates and adds the item in the inventory
        SetFirstEmptySlot(item, amount);
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

        for (var i = 0; i < newContainer.items.Length; i++) {
            container.items[i].UpdateSlot(newContainer.items[i].id, newContainer.items[i].item, newContainer.items[i].amount);
        }

        stream.Close();
        Debug.Log("Inventory loaded");
    }

    [ContextMenu("Clear")]
    public void Clear() {
        container = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    // List of all objects held in the inventory
    public InventorySlot[] items = new InventorySlot[GameUtils.InventorySize];
}

[System.Serializable]
public class InventorySlot
{
    public int id;
    public Item item;  // The scriptable object
    public int amount; // How much

    // Basic constructor
    public InventorySlot() {
        id = -1;
        item = null;
        amount = 0;
    }

    public InventorySlot(int id, Item item, int amount) {
        this.id = id;
        this.item = item;
        this.amount = amount;
    }

    public void UpdateSlot(int id, Item item, int amount) {
        this.id = id;
        this.item = item;
        this.amount = amount;
    }

    public void RemoveSlot() => UpdateSlot(-1, null, 0);

    public void Add(int val) {
        amount += val;
    }
}