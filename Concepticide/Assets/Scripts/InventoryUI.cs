using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory _inventory;
    private Transform _itemSlotContainer;

    private void Awake() {
        _itemSlotContainer = transform.Find("slots");
    }

    public void SetInventory(Inventory inv) {
        _inventory = inv;
    }
}