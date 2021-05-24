using UnityEngine;

public class PlayerTmp : MonoBehaviour
{
    [SerializeField]
    private InventoryUI inventoryUI;

    private Inventory _inventory;

    private void Awake() {
        _inventory = new Inventory();
        inventoryUI.SetInventory(_inventory);
    }
}