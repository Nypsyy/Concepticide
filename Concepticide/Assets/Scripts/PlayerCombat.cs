using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Option = MenuPanel.Option;

public class PlayerCombat : MonoBehaviour
{
    public string playerName;
    public float hp, mana, attack, defense, speed;
    private CombatManager _combatManager;

    private enum _SubMenu { Main, Attack, Magic, Objects };

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

    public void StartCombat(CombatManager manager) {
        _combatManager = manager;
    }

    public void StartTurn() {
        _SetMenu(_SubMenu.Main);
    }

    private void _SetMenu(_SubMenu menu) {

        switch(menu) {
            case _SubMenu.Main:
                _combatManager.menuPanel.DisplayMenu(new Option[]{
                    new Option("Attaque...", null, () => _SetMenu(_SubMenu.Attack)),
                    new Option("Défense", "Votre défense sera doublée pour le prochain tour.",
                                () => Debug.Log("Défense x2")),
                    new Option("Magie...", null, () => _SetMenu(_SubMenu.Magic)),
                    new Option("Objets...", null, () => _SetMenu(_SubMenu.Objects)),
                    new Option("Fuir", "Vous quitterez le combat.", () => Debug.Log("FUITE!")),
                });
                break;
            case _SubMenu.Attack:
                _combatManager.menuPanel.DisplayMenu(new Option[]{
                    new Option("Légère", "20 dégâts bruts", () => Debug.Log("LEGERE!")),
                    new Option("Lourde", "50 dégâts bruts", () => Debug.Log("LOURDE!")),
                    new Option("Retour...", "", () => _SetMenu(_SubMenu.Main)),
                });
                break;
            case _SubMenu.Magic:
                _combatManager.menuPanel.DisplayMenu(new Option[]{
                    new Option("Atq+", "Attaque += 5 pendant 2 tours", () => Debug.Log("ATQ+!")),
                    new Option("Déf+", "Défense += 5 pendant 2 tours", () => Debug.Log("DEF+!")),
                    new Option("Vit+", "Vitesse x2 pendant 2 tours", () => Debug.Log("VIT+!")),
                    new Option("Retour...", "", () => _SetMenu(_SubMenu.Main)),
                });
                break;
            case _SubMenu.Objects:
                _combatManager.menuPanel.DisplayMenu(new Option[]{
                    new Option("Retour...", "", () => _SetMenu(_SubMenu.Main)),
                });
                break;
        }
    }
}
