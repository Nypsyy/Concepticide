using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    public Concept m_Concept;

    public Dialogue dialogue1;
    private Dialogue dialogue2;

    void Start()
    {

    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if (m_Concept.isTradingAlive)
        {
            Debug.Log("j'ai clique sur " + this.gameObject.name);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
        }
    }
}
