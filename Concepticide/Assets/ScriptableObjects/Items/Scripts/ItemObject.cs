using UnityEngine;

// Items in the game
public enum ItemType
{
    HealthPotion,
    ManaPotion
}

// Base class for all scriptable objects items in the game
public abstract class ItemObject : ScriptableObject
{
    public int id;
    public Sprite uiDisplay;
    public ItemType type;

    [TextArea(15, 20)]
    public string description;
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;

    public Item(ItemObject item) {
        id = item.id;
        name = item.name;
    }
}