using System;
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


    public enum Action
    {
        // Actions
        LightAttack,
        HeavyAttack,
        Defense,
        Escape,

        // Magic spells
        MagicAttack,
        MagicDefense,
        MagicSpeed,

        // Consumables
        RegenHP,
        RegenMana,

        // Special
        DoNothing
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
        SetMenu(SubMenu.Main);
    }

    private enum SubMenu
    {
        Main,
        Attack,
        Magic,
        Objects
    }

    private void SetMenu(SubMenu menu) {
        switch (menu) {
            case SubMenu.Main:
                _combatManager.menuPanel.DisplayMenu(new[] {
                                                               new Option("Attaque...", "", () => SetMenu(SubMenu.Attack)),
                                                               new Option(
                                                                   "Défense", "Votre défense sera doublée pour le prochain tour.",
                                                                   () => _combatManager.EndPlayerTurn(Action.Defense)),
                                                               hasMagic
                                                                   ? new Option("Magie...", "", () => SetMenu(SubMenu.Magic))
                                                                   : Option.Null,
                                                               hasItems
                                                                   ? new Option("Objets...", "", () => SetMenu(SubMenu.Objects))
                                                                   : Option.Null,
                                                               new Option(
                                                                   "Fuir", "Vous quitterez le combat et garderez le métal amassé.",
                                                                   () => _combatManager.EndPlayerTurn(Action.Escape))
                                                           });
                break;

            case SubMenu.Attack:
                _combatManager.menuPanel.DisplayMenu(new[] {
                                                               new Option("Légère", "",
                                                                          () => _combatManager.EndPlayerTurn(Action.LightAttack)),
                                                               new Option("Lourde", "Attaque 3 fois plus forte, tour suivant perdu",
                                                                          () => _combatManager.EndPlayerTurn(Action.HeavyAttack)),
                                                               new Option("Retour...", "", () => SetMenu(SubMenu.Main))
                                                           });
                break;

            case SubMenu.Magic:
                _combatManager.menuPanel.DisplayMenu(new[] {
                                                               new Option("Atq+", "Attaque += 5 pendant 2 tours",
                                                                          () => _combatManager.EndPlayerTurn(Action.MagicAttack)),
                                                               new Option("Déf+", "Défense += 5 pendant 2 tours",
                                                                          () => _combatManager.EndPlayerTurn(Action.MagicDefense)),
                                                               new Option("Vit+", "Vitesse x2 pendant 2 tours",
                                                                          () => _combatManager.EndPlayerTurn(Action.MagicSpeed)),
                                                               new Option("Retour...", "", () => SetMenu(SubMenu.Main))
                                                           });
                break;

            case SubMenu.Objects:
                _combatManager.menuPanel.DisplayMenu(new[] {
                                                               new Option("Potion de vie", "PV + 50",
                                                                          () => _combatManager.EndPlayerTurn(Action.RegenHP)),
                                                               hasMagic
                                                                   ? new Option("Potion de magie", "Mana + 50",
                                                                                () => _combatManager.EndPlayerTurn(Action.RegenMana))
                                                                   : Option.Null,
                                                               new Option("Retour...", "", () => SetMenu(SubMenu.Main))
                                                           });
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(menu), menu, null);
        }
    }
}