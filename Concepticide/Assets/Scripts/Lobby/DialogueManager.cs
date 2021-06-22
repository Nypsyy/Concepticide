using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUtils;

public delegate void DialogueEvent(int nb);

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private int SentenceNb => _sentences.Count;

    public DialogueEvent onDisplaySentence;

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
        if (SentenceNb == 0) {
            EndDialogue();
            return;
        }

        var sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        onDisplaySentence?.Invoke(SentenceNb);
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