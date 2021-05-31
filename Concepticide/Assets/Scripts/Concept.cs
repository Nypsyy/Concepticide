using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concept : MonoBehaviour
{

    public Material natureMaterial0;
    public Material natureMaterial1;


    void Start()
    {
        natureMaterial0.SetColor("_Color", new Color(0.8f,0.3f,0.3f));
        natureMaterial1.SetColor("_Color", new Color(0.8f,0.35f,0.35f));
    }
    
    // Update is called once per frame
    void Update()
    {
    }

    void OnApplicationQuit()
    {
        natureMaterial0.SetColor("_Color", new Color(1f,1f,1f));
        natureMaterial1.SetColor("_Color", new Color(1f,1f,1f));
    }
}
