using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUtils;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public int SentenceNb => _sentences.Count;
    
    private Queue<string> _sentences;

    private void Start() {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool(AnimVariables.IsOpen, true);
        nameText.text = dialogue.name;

        _sentences.Clear();

        foreach (var sentence in dialogue.sentences) {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        // m_ShopPanel.SetActive(sentences.Count == 2);

        if (_sentences.Count == 0) {
            EndDialogue();
            return;
        }

        var sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue() {
        animator.SetBool(AnimVariables.IsOpen, false);
    }

    private IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (var letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }
}