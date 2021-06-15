using UnityEngine;

public struct GameUtils
{
    public static readonly int InventorySize = 12;
    
    public struct AnimVariables
    {
        public static readonly int Running = Animator.StringToHash("speed");
        public static readonly int IsOpen = Animator.StringToHash("isOpen");
    }
}