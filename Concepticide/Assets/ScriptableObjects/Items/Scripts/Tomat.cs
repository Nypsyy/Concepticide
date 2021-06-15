using UnityEngine;

[CreateAssetMenu(fileName = "New Tomat Object", menuName = "Inventory System/Items/Tomat", order = 3)]
public class Tomat : ItemObject
{
    public int attackBoost;

    private void Awake() {
        type = ItemType.ManaPotion;
    }
}