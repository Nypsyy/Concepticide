using UnityEngine;

public class Dealer : MonoBehaviour
{
    public Concept m_Concept;

    public Dialogue dialogue1;
    private Dialogue dialogue2;

    private void OnMouseDown() {
        if (!m_Concept.isTradingAlive) return;
        
        Debug.Log("Clicked on " + gameObject.name);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
    }
}
