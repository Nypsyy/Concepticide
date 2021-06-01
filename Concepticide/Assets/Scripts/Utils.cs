using UnityEngine;

public struct Utils
{
    public enum ItemType
    {
        HealthPotion,
        ManaPotion,
        SpeedPotion
    }

    public struct AnimVariables
    {
            public static readonly int running = Animator.StringToHash("speed");
    }
}