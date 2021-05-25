using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public Boss boss;

    public Text infoText;

    public MenuPanel menuPanel;



    public float playerHP { get; private set; }
    public float playerMana { get; private set; }
    public float bossHP { get; private set; }

    public delegate void CombatEndDelegate(bool hasPlayerWon);

    public CombatEndDelegate endDelegate;



    // Just after instantiating CombatManager, you must set public members 'playerCombat', 'boss' and 'endDelegate'.
    // The CombatManager will be active the next frame after construction.
    void Start()
    {
        // Invoking next frame to ensure all objects have started
        // and public members were initialized.
        Invoke("_StartCombat", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void _StartCombat() {
        menuPanel.PushInfo("Le combat vient de commencer !");

        playerHP = playerCombat.hp;
        playerMana = playerCombat.mana;
        bossHP = boss.hp;

        playerCombat.StartCombat(this);
        boss.StartCombat(this);

        menuPanel.DisplayInfo("Le combat commence !", () => {
            StartPlayerTurn();
        });
    }

    public void SetInfoText(string text) {
        infoText.text = text;
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

        playerCombat.StartTurn();
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
                playerMana -= 20;
                _magicAttackCooldown = 3;
                menuPanel.DisplayInfo($"Vous devenez temporairement plus fort ! (Attaque + 5 pendant 2 tours)", () => { boss.StartTurn(); });
                break;

            case PlayerCombat.Action.MagicDefense:
                if (playerMana < 20) {
                    menuPanel.DisplayInfo($"Vous n'avez pas assez de mana !", () => { playerCombat.StartTurn(); });
                    break;
                }
                _magicDefenseCooldown = 3;
                menuPanel.DisplayInfo($"Vous devenez temporairement plus résistant ! (Défense + 5 pendant 2 tours)", () => { boss.StartTurn(); });
                break;
                
            case PlayerCombat.Action.MagicSpeed:
                if (playerMana < 20) {
                    menuPanel.DisplayInfo($"Vous n'avez pas assez de mana !", () => { playerCombat.StartTurn(); });
                    break;
                }
                _magicSpeedCooldown = 3;
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
