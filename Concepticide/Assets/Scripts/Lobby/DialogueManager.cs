using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    public GameObject m_ShopPanel;

    private void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Starting conversation with " + dialogue.name);
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 2)
            m_ShopPanel.SetActive(true);
        else
            m_ShopPanel.SetActive(false);

        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue() {
        Debug.Log("end of conversation");
        animator.SetBool("isOpen", false);
    }

    private IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (var letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }
}