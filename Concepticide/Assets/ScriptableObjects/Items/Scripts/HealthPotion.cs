using UnityEngine;

[CreateAssetMenu(fileName = "New Health Potion Object", menuName = "Inventory System/Items/HealthPotion", order = 0)]
public class HealthPotion : ItemObject
{
    public int restoreHealth;

    private void Awake() {
        type = ItemType.HealthPotion;
    }
}