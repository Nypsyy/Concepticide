using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform otherPortalPos;

    public BossArena bossArena;
    public Concept.Id concept;

    private void OnTriggerEnter(Collider other) {
        if (bossArena == null) {
            Debug.Log("Portal not linked!");
            return;
        }

        bossArena.StartCombat(concept);
    }
}