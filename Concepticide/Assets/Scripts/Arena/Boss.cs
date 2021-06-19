using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public string bossName;
    public float hp, attack, defense, speed;
    public string[] attacks;

    public Canvas stats;
    public Slider healthBar;
    public Text statField;

    public enum Action
    {
        Attack
    }

    private CombatManager _combatManager;

    public void StartCombat(CombatManager manager) {
        _combatManager = manager;
        stats.enabled = true;
    }

    public void StopCombat() {
        stats.enabled = false;
    }


    public void StartTurn() {
        _combatManager.EndBossTurn(Action.Attack);
    }
}