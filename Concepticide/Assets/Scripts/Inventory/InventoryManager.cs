using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S))
            inventory.Save();

        if (Input.GetKeyDown(KeyCode.L))
            inventory.Load();
    }

    private void OnTriggerEnter(Collider other) {
        var item = other.GetComponent<GroundItem>();

        if (!item) return;

        inventory.AddItem(new Item(item.item), 1);
        Destroy(other.gameObject);
    }

    private void OnApplicationQuit() {
        inventory.container.items.Clear();
    }
}