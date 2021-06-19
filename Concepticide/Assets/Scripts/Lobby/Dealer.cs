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

    private Dialogue _dialogue2;

    private void OnEnable() {
        inventoryObject.Load();
    }

    private void Update() {
        shopKeeperInvUI.gameObject.SetActive(dialogueManager.SentenceNb == 1);
        playerInvUI.gameObject.SetActive(dialogueManager.SentenceNb == 1);
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