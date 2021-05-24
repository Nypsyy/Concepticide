using System.Collections.Generic;

public class Inventory
{
    private Dictionary<Utils.ItemType, uint> _items;
    public Dictionary<Utils.ItemType, uint> Items => _items;

    public Inventory() {
        _items = new Dictionary<Utils.ItemType, uint>();
    }

    public void AddItem(Utils.ItemType itemType, uint amount) {
        if (_items.ContainsKey(itemType)) {
            _items[itemType] += amount;
            return;
        }

        _items.Add(itemType, amount);
    }

    public void UseItem(Utils.ItemType itemType) {
        if (!_items.ContainsKey(itemType))
            return;

        _items[itemType]--;

        if (_items[itemType] <= 0)
            _items.Remove(itemType);
    }
}