using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraTest : MonoBehaviour
{

    public GameObject combatManagerPrefab;

    public PlayerCombat playerCombat;

    public Boss boss;

    public Canvas playerStats;
    public Canvas bossStats;

    public Slider playerHealthBar;
    public Slider playerManaBar;
    public Slider bossHealthBar;

    public Text playerStatField;
    public Text bossStatField;

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
        CombatManager newCombatManager = Instantiate(combatManagerPrefab, transform).GetComponent<CombatManager>();
        newCombatManager.boss = boss;
        newCombatManager.playerCombat = playerCombat;
        /*newCombatManager.playerStats = playerStats;
        newCombatManager.bossStats = bossStats;
        newCombatManager.playerHealthBar = playerHealthBar;
        newCombatManager.playerManaBar = playerManaBar;
        newCombatManager.bossHealthBar = bossHealthBar;
        newCombatManager.playerStatField = playerStatField;
        newCombatManager.bossStatField = bossStatField;*/
        newCombatManager.endDelegate = (hasPlayerWon) => {
            _StartCombat(); // doing combat forever in this case
        };
    }
}
