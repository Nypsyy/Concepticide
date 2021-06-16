using UnityEngine;

// Items in the game
public enum ItemType
{
    Helmet,
    Chest,
    Boots,
    Arms,
    HealthPotion,
    ManaPotion,
    Tomat
}

// Base class for all scriptable objects items in the game
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/Item", order = 1)]
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public ItemType type;
    public Item itemData = new Item();
    public bool stackable;

    [TextArea(15, 20)]
    public string description;
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;

    public Item() {
        id = -1;
        name = "";
    }

    public Item(ItemObject item) {
        id = item.itemData.id;
        name = item.name;
    }
}