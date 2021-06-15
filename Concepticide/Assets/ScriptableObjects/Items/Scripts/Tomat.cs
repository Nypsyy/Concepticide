using UnityEngine;

[CreateAssetMenu(fileName = "New Tomat Object", menuName = "Inventory System/Items/Tomat", order = 2)]
public class Tomat : ItemObject
{
    public int boostAtk;

    private void Awake() {
        type = ItemType.Tomat;
    }
}