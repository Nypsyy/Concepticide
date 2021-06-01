using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Option = MenuPanel.Option;

public class PlayerCombat : MonoBehaviour
{
    public string playerName;
    public float hp, mana, attack, defense, speed;

    public Canvas stats;
    public Slider healthBar;
    public Slider manaBar;

    public Text statField;

    public bool hasMagic = true;
    public bool hasItems = true;

    private CombatManager _combatManager;


    public enum Action {
        // Actions
        LightAttack, HeavyAttack, Defense, Escape,
        // Magic spells
        MagicAttack, MagicDefense, MagicSpeed, 
        // Consumables
        RegenHP, RegenMana,

        // Special
        DoNothing,
    };


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void StartCombat(CombatManager manager) {
        manaBar.gameObject.SetActive(hasMagic);
        _combatManager = manager;
        stats.enabled = true;
    }

    public void StopCombat() {
        stats.enabled = false;
    }


    public void StartTurn() {
        _SetMenu(_SubMenu.Main);
    }

    private enum _SubMenu { Main, Attack, Magic, Objects };
    private void _SetMenu(_SubMenu menu) {

        switch(menu) {
            case _SubMenu.Main:
                _combatManager.menuPanel.DisplayMenu(new Option[]{
                    new Option("Attaque...", "", () => _SetMenu(_SubMenu.Attack)),
                    new Option("Défense", "Votre défense sera doublée pour le prochain tour.",
                                () => _combatManager.EndPlayerTurn(Action.Defense)),
                    hasMagic ? new Option("Magie...", "", () => _SetMenu(_SubMenu.Magic)) : Option.NULL,
                    hasItems ? new Option("Objets...", "", () => _SetMenu(_SubMenu.Objects)) : Option.NULL,
                    new Option("Fuir", "Vous quitterez le combat.", () => _combatManager.EndPlayerTurn(Action.Escape)),
                });
                break;
            case _SubMenu.Attack:
                _combatManager.menuPanel.DisplayMenu(new Option[]{
                    new Option("Légère", "", () => _combatManager.EndPlayerTurn(Action.LightAttack)),
                    new Option("Lourde", "Attaque 3 fois plus forte, tour suivant perdu", () => _combatManager.EndPlayerTurn(Action.HeavyAttack)),
                    new Option("Retour...", "", () => _SetMenu(_SubMenu.Main)),
                });
                break;
            case _SubMenu.Magic:
                _combatManager.menuPanel.DisplayMenu(new Option[]{
                    new Option("Atq+", "Attaque += 5 pendant 2 tours", () => _combatManager.EndPlayerTurn(Action.MagicAttack)),
                    new Option("Déf+", "Défense += 5 pendant 2 tours", () => _combatManager.EndPlayerTurn(Action.MagicDefense)),
                    new Option("Vit+", "Vitesse x2 pendant 2 tours", () => _combatManager.EndPlayerTurn(Action.MagicSpeed)),
                    new Option("Retour...", "", () => _SetMenu(_SubMenu.Main)),
                });
                break;
            case _SubMenu.Objects:
                _combatManager.menuPanel.DisplayMenu(new Option[]{
                    new Option("Potion de vie", "PV + 50", () => _combatManager.EndPlayerTurn(Action.RegenHP)),
                    hasMagic ? new Option("Potion de magie", "Mana + 50", () => _combatManager.EndPlayerTurn(Action.RegenMana)) : Option.NULL,
                    new Option("Retour...", "", () => _SetMenu(_SubMenu.Main)),
                });
                break;
        }
    }
}
