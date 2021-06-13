using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] items;
    public Dictionary<ItemObject, int> getId = new Dictionary<ItemObject, int>();
    public Dictionary<int, ItemObject> getItem = new Dictionary<int, ItemObject>();

    public void OnBeforeSerialize() {
    }

    
    public void OnAfterDeserialize() {
        getId = new Dictionary<ItemObject, int>();
        getItem = new Dictionary<int, ItemObject>();
        
        for (var i = 0; i < items.Length; i++) {
            getId.Add(items[i], i);
            getItem.Add(i, items[i]);
        }
    }
}