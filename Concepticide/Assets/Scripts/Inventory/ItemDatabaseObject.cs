using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database", order = 0)]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] itemObjects;

    [ContextMenu("Update IDs")]
    public void UpdateIDs() {
        for (var i = 0; i < itemObjects.Length; i++) {
            if (itemObjects[i]?.itemData.id != i)
                itemObjects[i].itemData.id = i;
        }
    }

    public void OnBeforeSerialize() {
    }

    public void OnAfterDeserialize() {
        UpdateIDs();
    }
}