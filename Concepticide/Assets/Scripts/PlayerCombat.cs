using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public string playerName;
    public float hp, mana, attack, defense, speed;
    public CombatManager combatManager;
    public MenuPanel menuPanel;

    private enum _SubMenu { Main, Attack, Magic, Objects };

    // public PlayerInventory inventory;
    // public CombatMenu menu;
    // public Canvas menuCanvas;


    // Start is called before the first frame update
    void Start()
    {
        _SetMenu(_SubMenu.Main);
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

    private void _SetMenu(_SubMenu menu) {
        switch(menu) {
            case _SubMenu.Main:
                menuPanel.SetOptions(new string[]{"Attaque", "Défense", "Magie", "Objets", "Fuir"});
                menuPanel.selectionHandler = (optionName) => {
                    switch (optionName) {
                        case "Attaque": _SetMenu(_SubMenu.Attack); break;
                        case "Défense": combatManager.SetInfoText("Défense x2..."); break;
                        case "Magie": _SetMenu(_SubMenu.Magic); break;
                        case "Objets": _SetMenu(_SubMenu.Objects); break;
                        case "Fuir": combatManager.SetInfoText("Fuite!"); break;
                    }
                };
                break;
            case _SubMenu.Attack:
                menuPanel.SetOptions(new string[]{"Légère", "Lourde", "Retour"});
                menuPanel.selectionHandler = (optionName) => {
                    switch (optionName) {
                        case "Légère": combatManager.SetInfoText("Attaque légère..."); break;
                        case "Lourde": combatManager.SetInfoText("Attaque lourde..."); break;
                        case "Retour": _SetMenu(_SubMenu.Main); break;
                    }
                };
                break;
            case _SubMenu.Magic:
                menuPanel.SetOptions(new string[]{"Attq+", "Déf+", "Vit+", "Retour"});
                menuPanel.selectionHandler = (optionName) => {
                    switch (optionName) {
                        case "Attq+": combatManager.SetInfoText("Plus d'attaque..."); break;
                        case "Déf+": combatManager.SetInfoText("Plus de défense..."); break;
                        case "Vit+": combatManager.SetInfoText("Plus de vitesse..."); break;
                        case "Retour": _SetMenu(_SubMenu.Main); break;
                    }
                };
                break;
            case _SubMenu.Objects:
                menuPanel.SetOptions(new string[]{"Retour"});
                menuPanel.selectionHandler = (optionName) => {
                    switch (optionName) {
                        case "Retour": _SetMenu(_SubMenu.Main); break;
                    }
                };
                break;
        }
    }
}
