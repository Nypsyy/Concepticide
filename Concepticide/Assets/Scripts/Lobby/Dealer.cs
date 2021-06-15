using System;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    public Concept m_Concept;

    public Dialogue dialogue1;

    public Material border;
    public Material notBorder;
    
    private Dialogue dialogue2;
    
    private void OnMouseEnter() {
        GetComponentInChildren<Renderer>().material = border;
    }

    private void OnMouseExit() {
        GetComponentInChildren<Renderer>().material = notBorder;
    }

    private void OnMouseDown() {
        if (!m_Concept.isTradingAlive) return;
        
        Debug.Log("Clicked on " + gameObject.name);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
    }
}
