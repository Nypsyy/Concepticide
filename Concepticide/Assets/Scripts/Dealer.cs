using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{

    public Dialogue dialogue1;
    private Dialogue dialogue2;

    void Start()
    {

    }

    // Update is called once per frame
    void OnMouseDown()
    {
        Debug.Log("j'ai cliqué sur " + this.gameObject.name);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
    }
}
