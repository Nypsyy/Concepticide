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
        Debug.Log("j'ai cliqu� sur " + this.gameObject.name);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            Debug.Log("j'ai cliqu� sur ");
        }

        if(Input.GetKeyDown("h"))
        {
            Debug.Log("oui");
        }
    }
    
}
