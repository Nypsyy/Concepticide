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
            public static readonly int IsRunning = Animator.StringToHash("isRunning");
    }
}