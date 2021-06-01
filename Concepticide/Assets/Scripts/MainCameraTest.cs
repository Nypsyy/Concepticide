using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraTest : MonoBehaviour
{

    public CombatManager combatManager;

    public PlayerCombat playerCombat;

    public Boss boss;


    // Start is called before the first frame update
    void Start()
    {
        _StartCombat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _StartCombat() {
        /*CombatManager newCombatManager = Instantiate(combatManagerPrefab, transform).GetComponent<CombatManager>();
        newCombatManager.boss = boss;
        newCombatManager.playerCombat = playerCombat;
        /*newCombatManager.playerStats = playerStats;
        newCombatManager.bossStats = bossStats;
        newCombatManager.playerHealthBar = playerHealthBar;
        newCombatManager.playerManaBar = playerManaBar;
        newCombatManager.bossHealthBar = bossHealthBar;
        newCombatManager.playerStatField = playerStatField;
        newCombatManager.bossStatField = bossStatField;
        newCombatManager.endDelegate = (hasPlayerWon) => {
            _StartCombat(); // doing combat forever in this case
        };
        newCombatManager.enabled = true;
        newCombatManager.StartCombat();*/
        combatManager.boss = boss;
        combatManager.playerCombat = playerCombat;
        combatManager.gameObject.SetActive(true);
        combatManager.endDelegate = (hasPlayerWon) => {
            _StartCombat(); // doing combat forever in this case
        };
        combatManager.StartCombat();
    }
}
