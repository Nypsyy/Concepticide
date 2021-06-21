using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenWander : MonoBehaviour
{

    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Mathf.Abs(transform.position.x - target.transform.position.x)<=0.5 && Mathf.Abs(transform.position.z - target.transform.position.z) <= 0.5)
        {
            target.transform.position = new Vector3(target.transform.position.x + Random.Range(-1,1), target.transform.position.y, target.transform.position.z + Random.Range(-1, 1));
        }



    }
}
