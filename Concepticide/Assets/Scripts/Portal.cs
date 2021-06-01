using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform otherPortalPos;

    public BossArena bossArena;
    public Concept.Id concept;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bossArena == null) {
            Debug.Log("Portal not linked!");
            return;
        }
        bossArena.StartCombat(concept);
    }
    
}
