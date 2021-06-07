using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    public bool direction;
    public float speedRotation = 15f;
    
    void FixedUpdate()
    {
        int coefficient;
        if (direction)
            coefficient = 1;
        else
            coefficient = -1;
        
        var eulerAngles = Vector3.up * (speedRotation * coefficient);
        transform.Rotate(eulerAngles * Time.deltaTime, Space.Self);
    }
}
