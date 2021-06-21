using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
