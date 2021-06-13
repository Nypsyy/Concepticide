using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory", order = 0)]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    private ItemDatabaseObject _database;
    public string savePath;

    // List of all objects held in the inventory
    public List<InventorySlot> container = new List<InventorySlot>();

    // Link the correct database
    private void OnEnable() {
#if UNITY_EDITOR
        _database = (ItemDatabaseObject) AssetDatabase.LoadAssetAtPath("Assets/Scriptable Objects/Items/Database.asset",
                                                                       typeof(ItemDatabaseObject));
#else
        _database = Resources.Load<ItemDatabaseObject>("Inventory/Database");
#endif
    }

    public void Save() {
        // Json format
        var saveData = JsonUtility.ToJson(this, true);
        // Create a saving file to a common path no matter the platform
        var bf = new BinaryFormatter();
        var fs = File.Create(string.Concat(Application.persistentDataPath, savePath));

        // Serializing and closing
        bf.Serialize(fs, saveData);
        fs.Close();
    }

    public void Load() {
        // Checking the file
        if (!File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            return;

        // Opening
        var bf = new BinaryFormatter();
        var fs = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);

        // Deserializing and formatting for object
        JsonUtility.FromJsonOverwrite(bf.Deserialize(fs).ToString(), this);
        fs.Close();
    }

    public void AddItem(ItemObject item, int amount) {
        // Adds the amount if the item already exists
        foreach (var slot in container.Where(slot => slot.item == item)) {
            slot.Add(amount);
            return;
        }

        // Else creates and adds the item in the inventory
        container.Add(new InventorySlot(_database.getId[item], item, amount));
    }

    public void OnBeforeSerialize() {
    }

    public void OnAfterDeserialize() {
        foreach (var slot in container) {
            slot.item = _database.getItem[slot.id];
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public int id;
    public ItemObject item; // The scriptable object
    public int amount;      // How much

    // Basic constructor
    public InventorySlot(int id, ItemObject item, int amount) {
        this.id = id;
        this.item = item;
        this.amount = amount;
    }

    public void Add(int val) {
        amount += val;
    }
}