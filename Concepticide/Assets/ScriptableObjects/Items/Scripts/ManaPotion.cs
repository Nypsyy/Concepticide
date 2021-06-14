using UnityEngine;

[CreateAssetMenu(fileName = "New Mana Potion Object", menuName = "Inventory System/Items/Mana", order = 1)]
public class ManaPotion : ItemObject
{
    public int restoreMana;

    private void Awake() {
        type = ItemType.ManaPotion;
    }
}