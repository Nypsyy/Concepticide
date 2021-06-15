using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUtils;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    public GameObject shopKeeperInventory;
    public GameObject playerInventory;

    private void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool(AnimVariables.IsOpen, true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (var sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        shopKeeperInventory.SetActive(sentences.Count == 2);
        playerInventory.SetActive(sentences.Count == 2);
        // m_ShopPanel.SetActive(sentences.Count == 2);

        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        var sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue() {
        animator.SetBool(AnimVariables.IsOpen, false);
        shopKeeperInventory.SetActive(false);
        playerInventory.SetActive(false);
    }

    private IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (var letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }
}