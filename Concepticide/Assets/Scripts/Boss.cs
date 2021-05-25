using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public string bossName;
    public float hp, attack, defense, speed;
    public string[] attacks;

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
    }
    public void StartTurn() {
        _combatManager.menuPanel.DisplayInfo("Le boss ne fait rien (ses attaques ne sont pas implémentées).", () => {
            _combatManager.StartPlayerTurn();
        });
    }
}
