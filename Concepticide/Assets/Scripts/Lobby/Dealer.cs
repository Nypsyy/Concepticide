using UnityEngine;

public class Dealer : MonoBehaviour
{
    public Concept m_Concept;
    public Dialogue dialogue1;

    private Dialogue dialogue2;

    private void OnMouseEnter() {
        GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit() {
        GetComponent<Outline>().enabled = false;
    }

    private void OnMouseDown() {
        if (!m_Concept.isTradingAlive) return;

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
    }
}