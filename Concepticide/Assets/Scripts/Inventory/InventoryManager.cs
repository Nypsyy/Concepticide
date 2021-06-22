using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    public GameObject inventoryScreen;
    public GameObject equipmentScreen;

    private void Update() {
        // TODO: CORRECTION AFFICHAGE INVENTAIRE
        inventoryScreen.SetActive(true);
        if (!Input.GetKeyDown(KeyCode.Tab))
            return;

        equipmentScreen.SetActive(!equipmentScreen.activeSelf);
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("GroundItem"))
            return;

        if (inventory.AddItem(new Item(other.GetComponent<GroundItem>().item), 1)) {
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit() {
        inventory.container.Clear();
        equipment.container.Clear();
    }
}