using System;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    public bool direction, x, y, z;
    public float speedRotation = 15f;

    private void FixedUpdate() {
        int coefficient;
        
        if (direction)
            coefficient = 1;
        else
            coefficient = -1;

        var directionRotation = new Vector3(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(z));
        var eulerAngles = directionRotation * (speedRotation * coefficient);
        transform.Rotate(eulerAngles * Time.deltaTime, Space.Self);
    }
}