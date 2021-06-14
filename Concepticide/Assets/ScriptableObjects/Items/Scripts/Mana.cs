using UnityEngine;

[CreateAssetMenu(fileName = "New Mana Object", menuName = "Inventory System/Items/Mana", order = 1)]
public class Mana : ItemObject
{
    public int restoreMana;

    private void Awake() {
        type = ItemType.Mana;
    }
}