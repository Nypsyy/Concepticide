using System;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    public Concept concept;
    public DialogueManager dialogueManager;
    public Dialogue dialogue1;
    public InventoryObject inventoryObject;
    public GameObject shopKeeperInvUI;
    public GameObject playerInvUI;
    public GameObject playerEquipUI;

    private Dialogue _dialogue2;

    private void Awake() {
        inventoryObject.Load();
        dialogueManager.onDisplaySentence += ShowInventories;
    }

    private void ShowInventories(int sentenceNb) {
        shopKeeperInvUI.gameObject.SetActive(sentenceNb == 1);
        playerInvUI.gameObject.SetActive(sentenceNb == 1);
        playerEquipUI.gameObject.SetActive(sentenceNb == 1);
    }

    private void OnMouseEnter() {
        GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit() {
        GetComponent<Outline>().enabled = false;
    }

    private void OnMouseDown() {
        if (!concept.isTradingAlive) return;

        dialogueManager.StartDialogue(dialogue1);
    }
}