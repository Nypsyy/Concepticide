using UnityEngine;

// Item type in the game
public enum ItemType
{
    Health,
    Mana
}

// Base class for all scriptable objects items in the game
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;

    [TextArea(15, 20)]
    public string description;
}