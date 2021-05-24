using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/Item", order = 0)]
public class Item : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public Utils.ItemType itemType;
}