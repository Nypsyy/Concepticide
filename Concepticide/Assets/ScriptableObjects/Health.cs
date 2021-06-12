using UnityEngine;

[CreateAssetMenu(fileName = "New Health Object", menuName = "Inventory System/Items/Health", order = 0)]
public class Health : ItemObject
{
    public int restoreHealth;

    private void Awake() {
        type = ItemType.Health;
    }
}