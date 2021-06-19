using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct GameUtils
{
    public const int InventorySize = 12;

    public struct MaterialVariables
    {
        public static readonly int NatureColor = Shader.PropertyToID("_Color");
    }

    public struct AnimVariables
    {
        public static readonly int Dead = Animator.StringToHash("Dead");
        public static readonly int StartNight = Animator.StringToHash("StartNight");
        public static readonly int Running = Animator.StringToHash("speed");
        public static readonly int IsOpen = Animator.StringToHash("isOpen");
    }
}

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> slotsOnInterface) {
        foreach (var slot in slotsOnInterface) {
            // There is an item
            if (slot.Value.item.id >= 0) {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.Value.ItemObject.uiDisplay;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = slot.Value.amount == 1 ? "" : slot.Value.amount.ToString("n0");
            }
            // No items ==> clear the slot
            else {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
}