using System.Collections.Generic;
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

    [ContextMenu("Save")]
    public void Save() {
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
    }

    [ContextMenu("Load")]
    public void Load() {
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
        container = (Inventory) formatter.Deserialize(stream);
        stream.Close();
    }

    [ContextMenu("Clear")]
    public void Clear() {
        container = new Inventory();
    }

    public void AddItem(Item item, int amount) {
        // Adds the amount if the item already exists
        foreach (var slot in container.items.Where(slot => slot.id == item.id)) {
            slot.Add(amount);
            return;
        }

        // Else creates and adds the item in the inventory
        container.items.Add(new InventorySlot(item.id, item, amount));
    }
}

[System.Serializable]
public class Inventory
{
    // List of all objects held in the inventory
    public List<InventorySlot> items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int id;
    public Item item;  // The scriptable object
    public int amount; // How much

    // Basic constructor
    public InventorySlot(int id, Item item, int amount) {
        this.id = id;
        this.item = item;
        this.amount = amount;
    }

    public void Add(int val) {
        amount += val;
    }
}