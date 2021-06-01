using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public Boss boss;


    public MenuPanel menuPanel;

    private float _playerHP;
    private float _playerMana;
    private float _bossHP; 

    public float playerHP { 
        get { return _playerHP; } 
        private set 
        {
            _playerHP = value;
            playerCombat.healthBar.value = value;
        } 
    }

    public float playerMana { 
        get { return _playerMana; } 
        private set 
        {
            _playerMana = value;
            playerCombat.manaBar.value = value;
        } 
    }

    public float bossHP {
        get { return _bossHP; } 
        private set{
            _bossHP = value;
            boss.healthBar.value = value;;
        } 
    }

    public delegate void CombatEndDelegate(bool hasPlayerWon);

    public CombatEndDelegate endDelegate;



    // Just after instantiating CombatManager, you must set public members 'playerCombat', 'boss' and 'endDelegate'.
    // The CombatManager will be active the next frame after construction.
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartCombat() {
        gameObject.SetActive(true);
        menuPanel.enabled = true;

        menuPanel.PushInfo("Le combat vient de commencer !");

        playerHP = playerCombat.hp;
        playerMana = playerCombat.mana;
        bossHP = boss.hp;

        UpdateStatsUI();
        playerCombat.StartCombat(this);
        boss.StartCombat(this);

        menuPanel.DisplayInfo("Le combat commence !", () => {
            StartPlayerTurn();
        });
    }


    public void UpdateStatsUI()
    {
        string playerStatString = "ATK : " + playerCombat.attack;
        if (_magicAttackCooldown > 0)
        {
            playerStatString += " + 5 = " + (playerCombat.attack + 5);
        }
        playerStatString += "\nDEF : " + playerCombat.defense;
        if(_magicDefenseCooldown > 0)
        {
            playerStatString += " + 5 = " + (playerCombat.defense + 5);
            if (_defenseCooldown > 0)
            {
                playerStatString += " * 2 = " + ((playerCombat.defense + 5) * 2);
            }        
        }
        else
        {
            if (_defenseCooldown > 0)
            {
                playerStatString += " * 2 = " + (playerCombat.defense * 2);
            }
        }

        playerCombat.statField.text = playerStatString;
        boss.statField.text = "ATK : " + boss.attack + "\nDEF : " + boss.defense;
    }

    private int _heavyAttackCooldown = 0;
    private int _magicAttackCooldown = 0;
    private int _magicDefenseCooldown = 0;
    private int _magicSpeedCooldown = 0;
    private int _defenseCooldown = 0;

    public void StartPlayerTurn() {
        
        if (_magicAttackCooldown > 0) {
            --_magicAttackCooldown;
            if (_magicAttackCooldown == 0) {
                menuPanel.DisplayInfo($"Votre bonus d'attaque s'estompe...", () => {
                    StartPlayerTurn();
                });
                return;
            }
        }
        if (_magicDefenseCooldown > 0) {
            --_magicDefenseCooldown;
            if (_magicDefenseCooldown == 0) {
                menuPanel.DisplayInfo($"Votre bonus de défense s'estompe...", () => {
                    StartPlayerTurn();
                });
                return;
            }
        }
        if (_magicSpeedCooldown > 0) {
            --_magicSpeedCooldown;
            if (_magicSpeedCooldown == 0) {
                menuPanel.DisplayInfo($"Votre bonus de vitesse s'estompe...", () => {
                    StartPlayerTurn();
                });
                return;
            }
        }
        if (_defenseCooldown > 0) --_defenseCooldown;
        
        if (_heavyAttackCooldown > 0) {
            --_heavyAttackCooldown;
            menuPanel.DisplayInfo($"Vous passez le tour à récupérer de l'attaque lourde...", () => {
                EndPlayerTurn(PlayerCombat.Action.DoNothing);
            });
            return;
        }
        UpdateStatsUI();
        playerCombat.StartTurn();
    }

    public void EndBossTurn(Boss.Action action) {
        switch (action) {
            case Boss.Action.Attack:
                float defense = playerCombat.defense;
                if (_magicDefenseCooldown > 0) defense += 5;
                if (_defenseCooldown > 0) defense *= 2;
                float damage = boss.attack - defense;
                if (damage < 0) damage = 0;
                playerHP = Mathf.Max(0, playerHP - damage);
                menuPanel.DisplayInfo($"Le {boss.bossName} vous attaque et vous inflige {damage} dégâts !", () => {
                    if (playerHP == 0) {
                        menuPanel.DisplayInfo($"Vous avez été tué...", () => {
                            menuPanel.enabled = false;
                            playerCombat.StopCombat();
                            boss.StopCombat();
                            gameObject.SetActive(false);
                            if (endDelegate != null)
                                endDelegate(false); // hasPlayerWon = false;
                        });
                    } else {
                        StartPlayerTurn();
                    }
                });
                break;
        }
    }

    // PlayerCombat est responsable d'appeler cette méthode à la fin de son tour
    public void EndPlayerTurn(PlayerCombat.Action action) {
        switch (action) {
            case PlayerCombat.Action.DoNothing:
                boss.StartTurn();
                break;
                
            case PlayerCombat.Action.LightAttack:
            case PlayerCombat.Action.HeavyAttack:
                float damage = playerCombat.attack - boss.defense;
                if (_magicAttackCooldown > 0) damage += 5;
                if (action == PlayerCombat.Action.HeavyAttack) {
                    damage *= 3;
                    _heavyAttackCooldown = 1;
                }
                if (damage < 0) damage = 0;
                bossHP = Mathf.Max(0, bossHP - damage);
                menuPanel.DisplayInfo($"Vous attaquez le {boss.bossName}, et lui infligez {damage} dégâts !", () => {
                    if (bossHP == 0) {
                        menuPanel.DisplayInfo($"Le {boss.bossName} a été tué, félicitations !", () => {
                            menuPanel.enabled = false;
                            playerCombat.StopCombat();
                            boss.StopCombat();
                            gameObject.SetActive(false);
                            if (endDelegate != null)
                                endDelegate(true); // hasPlayerWon = true
                        });
                    } else {
                        boss.StartTurn();
                    }
                });
                break;

            case PlayerCombat.Action.Defense:
                _defenseCooldown = 1;
                UpdateStatsUI();
                menuPanel.DisplayInfo($"Vous vous préparez à défendre. (Défense x 2)", () => { boss.StartTurn(); });
                break;

            case PlayerCombat.Action.Escape:
                // For now, let's ask the player another action
                menuPanel.DisplayInfo($"La fuite est impossible !", () => { playerCombat.StartTurn(); });
                break;

            case PlayerCombat.Action.MagicAttack:
                if (playerMana < 20) {
                    menuPanel.DisplayInfo($"Vous n'avez pas assez de mana !", () => { playerCombat.StartTurn(); });
                    break;
                }
                playerMana -= 30;
                _magicAttackCooldown = 3;
                UpdateStatsUI();
                menuPanel.DisplayInfo($"Vous devenez temporairement plus fort ! (Attaque + 5 pendant 2 tours)", () => { boss.StartTurn(); });
                break;

            case PlayerCombat.Action.MagicDefense:
                if (playerMana < 20) {
                    menuPanel.DisplayInfo($"Vous n'avez pas assez de mana !", () => { playerCombat.StartTurn(); });
                    break;
                }
                playerMana -= 30;
                _magicDefenseCooldown = 3;
                UpdateStatsUI();
                menuPanel.DisplayInfo($"Vous devenez temporairement plus résistant ! (Défense + 5 pendant 2 tours)", () => { boss.StartTurn(); });
                break;
                
            case PlayerCombat.Action.MagicSpeed:
                if (playerMana < 20) {
                    menuPanel.DisplayInfo($"Vous n'avez pas assez de mana !", () => { playerCombat.StartTurn(); });
                    break;
                }
                playerMana -= 30;
                _magicSpeedCooldown = 3;
                UpdateStatsUI();
                menuPanel.DisplayInfo($"Vous devenez temporairement plus rapide ! (Pas implémenté...)", () => { boss.StartTurn(); });
                break;
                
            case PlayerCombat.Action.RegenHP:
                playerHP = Mathf.Min(playerCombat.hp, playerHP + 50);
                menuPanel.DisplayInfo($"Vous utilisez une potion de vie et regagnez 50 PVs.", () => { boss.StartTurn(); });
                break;

            case PlayerCombat.Action.RegenMana:
                playerMana = Mathf.Min(playerCombat.mana, playerMana + 50);
                menuPanel.DisplayInfo($"Vous utilisez une potion de mana et regagnez 50 mana.", () => { boss.StartTurn(); });
                break;
            
        }
    }
}
