using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{

    public Dialogue dialogue1;
    private Dialogue dialogue2;

    // Update is called once per frame
    void OnMouseDown()
    {
        Debug.Log("j'ai cliqu� sur " + this.gameObject.name);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
    }
    
}
