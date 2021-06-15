using UnityEngine;

[CreateAssetMenu(fileName = "New Mana Potion Object", menuName = "Inventory System/Items/ManaPotion", order = 2)]
public class ManaPotion : ItemObject
{
    public int restoreMana;

    private void Awake() {
        type = ItemType.ManaPotion;
    }
}