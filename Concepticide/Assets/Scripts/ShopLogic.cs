using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    public LootCount lootCount;
    public ItemObject healthItem;
    public ItemObject manaItem;
    public InventoryObject inventory;
    
    public void OnMouseEnterButton() {
        lootCount.SetCost(7);
    }
    
    public void OnMouseExitButton() {
        lootCount.SetCost(0);
    }
    
    public void OnMouseClickLife() {
        if (!lootCount.ApplyCost()) return;
        inventory.AddItem(healthItem.itemData, 1);
    }
    
    public void OnMouseClickMana() {
        if (!lootCount.ApplyCost()) return;
        inventory.AddItem(manaItem.itemData, 1);
    }
}
