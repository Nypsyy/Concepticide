using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject inventoryScreen;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
            inventoryScreen.SetActive(!inventoryScreen.activeSelf);
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("GroundItem"))
            return;
        
        inventory.AddItem(new Item(other.GetComponent<GroundItem>().item), 1);
        Destroy(other.gameObject);
    }

    private void OnApplicationQuit() {
        inventory.container.items.Clear();
    }
}