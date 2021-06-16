using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database", order = 0)]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] items;

    [ContextMenu("Update IDs")]
    public void UpdateIDs() {
        for (var i = 0; i < items.Length; i++) {
            if (items[i]?.itemData.id != i)
                items[i].itemData.id = i;
        }
    }

    public void OnBeforeSerialize() {
    }

    public void OnAfterDeserialize() {
        UpdateIDs();
    }
}