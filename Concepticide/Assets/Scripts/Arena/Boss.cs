using System.Collections;
using System.Collections.Generic;
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

    public enum Action { Attack };

    private CombatManager _combatManager;

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
        stats.enabled = true;
        
    }

    public void StopCombat() {
        stats.enabled = false;
    }


    public void StartTurn() {
        _combatManager.EndBossTurn(Action.Attack);
    }
}
