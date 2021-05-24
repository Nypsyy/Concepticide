using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public string playerName;
    public float hp, mana, attack, defense, speed;
    public CombatManager combatManager;
    // public PlayerInventory inventory;
    // public CombatMenu menu;
    // public Canvas menuCanvas;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTurn() {
        // menuCanvas.setActive(true);
        
        // menuCanvas.addChild("Attaque");
        // menuCanvas.addChild("Défense");
        // menuCanvas.addChild("Magie");
        // menuCanvas.addChild("Objets");
    }

    public void WhenValidation() {

        // menuCanvas.setActive(false);
        // combatManager.AttackBoss(damage);
        combatManager.EndTurn();
    }
}
